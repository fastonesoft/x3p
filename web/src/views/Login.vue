<template>
    <dev-login>
        <div class="login-code" slot="passLogin">
            <div class="login-form">
                <i-col class="login-form-title">校务在线管理系统</i-col>
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
                        <Button type="primary" size="large" @click="loginClick('form')" long
                                style="background-color: #378CBE;">登录
                        </Button>
                    </FormItem>
                </Form>
            </div>
        </div>
    </dev-login>
</template>

<script>
    import devLogin from '../components/dev-login'
    import xcon from '../libs/xcon'

    export default {
        name: "login",
        components: {devLogin},
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
                            xcon.posts('/home/login', {
                                id: this.form.id,
                                pass: xcon.md5(this.form.pass),
                                token: xcon.md5(this.token + xcon.md5(this.form.pass))
                            })
                                .then(res => {
                                    // 记录用户信息
                                    this.$store.commit('userSet', res.user);
                                    this.$store.commit('typeSet', res.types);
                                    this.$store.commit('menuSet', res.menus);
                                    this.$store.commit('timeSet', new Date().toLocaleString());
                                    // 写入本地
                                    xcon.stateWrite(this.$store.state);
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
            xcon.stateClear();
            xcon.gets('/home/token')
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


    .login-code {
        position: fixed;
        left: 50%;
        top: 50%;
        width: 586px;
        height: 322px;
        margin-left: -293px;
        margin-top: -191px;
        background: url(../assets/login_auth.png);
        border-radius: 5px;
        box-shadow: 1px 1px 5px #333333, -1px -1px 5px #333333;
    }

    .login-form {
        margin-left: 206px;
        width: 380px;
        height: 322px;
        padding: 30px 40px;
    }

    .login-form-title {
        height: 70px;
        text-align: center;
        letter-spacing: 5px;
        text-shadow: 1px 1px 3px #666;
        color: #197BAB;
        font-size: 24px;
    }

</style>