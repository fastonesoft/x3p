<template>
    <div class="form">
        <div class="title">{{title}}</div>
        <Form ref="form" :model="form" :rules="rule">
            <FormItem prop="id">
                <Input type="text" prefix="md-contact" v-model="form.id" size="large" :maxlength="20"
                       placeholder="输入帐号，5-20个字符"/>
            </FormItem>
            <FormItem prop="pass">
                <Input type="password" prefix="ios-eye" v-model="form.pass" size="large" :maxlength="20"
                       placeholder="输入密码，6-20个字符"/>
            </FormItem>
            <FormItem>
                <Button type="primary" size="large" @click="loginClick('form')" long>登录</Button>
            </FormItem>
        </Form>
    </div>
</template>

<script>
    import $ from '../libs/xcon'

    export default {
        name: "LogonUser",

        props: {
            title: {
                type: String,
                default: '校务在线管理系统'
            },
            postUrl: {
                type: String,
                default: '/web/login'
            },
            tokenUrl: {
                type: String,
                default: '/web/token'
            },
        },

        data() {
            return {
                token: '',
                form: {
                    id: '',
                    pass: '',
                },
                rule: {
                    id: [
                        {
                            required: true, message: '请输入用户账号', trigger: 'change'
                        },
                        {
                            type: 'string',
                            min: 5,
                            max: 20,
                            message: '最小5位，最大20位',
                            trigger: 'change'
                        },
                        {
                            pattern: /^\w+$/,
                            message: '必须是字母、数字或者下划线',
                            trigger: 'change'
                        },
                    ],
                    pass: [
                        {
                            required: true, message: '请输入用户密码', trigger: 'change'
                        },
                        {
                            type: 'string',
                            min: 6,
                            max: 20,
                            message: '密码长度最小6位，最大20位',
                            trigger: 'change'
                        },
                        {
                            pattern: /^[\\.\w]+$/,
                            message: '必须是字母、数字或者下划线',
                            trigger: 'change'
                        },
                    ]
                },
            }
        },
        methods:
            {
                loginClick(name) {
                    this.$refs[name].validate((valid) => {
                        if (valid) {
                            // 提交this.form服务器端认证
                            $.posts(this.postUrl, {
                                id: this.form.id,
                                pass: $.md5(this.form.pass),
                                token: $.md5(this.token + $.md5(this.form.pass))
                            })
                                .then(res => {
                                    // 记录用户信息
                                    this.$store.commit('userSet', res.user);
                                    this.$store.commit('typeSet', res.types);
                                    this.$store.commit('menuSet', res.menus);
                                    this.$store.commit('timeSet', new Date().toLocaleString());
                                    // 写入本地
                                    $.stateWrite(this.$store.state);
                                    this.$router.replace('/vhome');
                                })
                                .catch(error => {
                                    this.$Message.error(error);
                                });
                        } else {
                            this.$Message.error('登录信息无法通过验证，请重新输入');
                        }
                    })

                }
            },
        created() {
            $.stateClear();
            $.gets(this.tokenUrl)
                .then(res => {
                    // token
                    this.token = res;
                })
                .catch(error => {
                    this.$Message.error(error);
                });
        }
    }
</script>

<style scoped>
    .form {
        margin-left: 206px;
        width: 380px;
        height: 322px;
        padding: 30px 40px;
    }

    .title {
        margin-bottom: 30px;
        text-align: center;
        letter-spacing: 5px;
        text-shadow: 1px 1px 3px #666;
        color: #197BAB;
        font-size: 28px;
    }
</style>