<template>
    <Layout class="content-body">
        <Sider
                class="sider"
                v-model="isCollaped"
                width="200"
                :class="{'sider-hide': isCollaped}"
                collapsible
        >
            <Row>
                <i-col class="sider-logo-center" :class="{'sider-hide': isCollaped}">
                    <img src="../assets/logo.png" alt="" class="sider-logo" :class="{'sider-hide': isCollaped}">
                </i-col>
            </Row>
            <Menu
                    theme="dark"
                    width="200"
                    :active-name="activeName"
            >
                <MenuGroup :title="type.type_name" v-for="type of types" v-bind:key="type.type_id">
                    <MenuItem
                            class="hidden-nowrap"
                            :name="menu.name"
                            :to="menu.name"
                            v-for="menu of menus.filter(menu => menu.type_id === type.type_id)"
                            v-bind:key="menu.id"
                            replace>
                        <Icon :type="menu.icon"/>
                        <span>{{menu.title}}</span>
                    </MenuItem>
                </MenuGroup>
            </Menu>
        </Sider>
        <Layout 
            :class="{'sider-hide': isCollaped}"
        >
            <Header class="header" style="background: #fff;">
                <Row>
                    <i-col span="19" class="header-title">
dfgsdfg
                    </i-col>
                    <i-col span="5" class="hidden-nowrap" style="text-align: right;">
                        <Tooltip placement="left" transfer>
                            <Tag color="error" v-if="user && user.name">{{ user.name }}</Tag>
                            <div slot="content" v-if="user && user.group_name">{{ user.group_name }}</div>
                        </Tooltip>
                        <Dropdown @on-click="downMenuClick" class="margin-left16" transfer>
                            <Avatar style="background-color: #87d068" icon="ios-person"/>
                            <DropdownMenu slot="list">
                                <DropdownItem name="set">
                                    个人设置
                                    <Badge status="error"></Badge>
                                </DropdownItem>
                                <DropdownItem name="logout" divided>退出登录</DropdownItem>
                            </DropdownMenu>
                        </Dropdown>
                        <Badge :count="count" :offset="[20,4]" class="margin-left16 margin-right16">
                            <Icon type="md-notifications-outline" size="24"/>
                        </Badge>
                    </i-col>
                </Row>
            </Header>
            <Content class="content">
                <slot></slot>
            </Content>
            <Row class="footer align-right"
                :class="{'sider-hide': isCollaped}"
                style="background: #fff;"
            >
            2019 &copy; Fastone
            </Row>
        </Layout>
    </Layout>
</template>

<script>
    import $ from "../libs/xcon";

    export default {
        data() {
            return {
                activeName: this.$route.path,
                count: 0,
                isCollaped: false,
                Types: [],
            };
        },

        computed: {
            user() {
                return this.$store.state.user;
            },
            types() {
                return this.$store.state.types;
            },
            menus() {
                return this.$store.state.menus;
            }
        },
        created() {
            this.activeName = this.$route.path;
        },

        methods: {
            downMenuClick(name) {
                switch (name) {
                    case 'logout': {
                        this.$Message.info('退出登录');

                        $.gets('/home/logout')
                            .then(() => {
                                // 清除用户信息
                                this.$store.commit('userSet', null);
                                this.$store.commit('typeSet', []);
                                this.$store.commit('menuSet', []);
                                this.$store.commit('timeSet', null);
                                // 清楚session
                                $.stateClear();
                                this.$router.replace('/vlogin');
                            })
                            .catch(error => {
                                this.$Message.error(error);
                            });

                        break;
                    }
                }

            }
        }
    }
</script>

<style>

    .content-body {
        height: 100%;
    }

    /*侧边菜单设置*/
    .sider {
        z-index: 2;
    }

    .sider-logo-center {
        text-align: center;
        background: #515a6e;
        position: fixed!important;
        top: 0;
        left: 0;
        height: 85px;
        width: 200px;
        transition: all .2s ease-in-out;
    }
    .sider-hide .sider-logo-center {
        width: 64px;
        height: 40px;
        transition: all .2s ease-in-out;
    }

    .sider-logo {
        width: 60px;
        height: 60px;
        margin: 10px auto;
        transition: all .2s ease-in-out;
    }

    .sider-hide .sider-logo {
        width: 35px;
        height: 35px;
        transition: all .2s ease-in-out;
    }

    .ivu-badge-count {
        cursor: default;
    }

    .ivu-menu-item-group-title {
        cursor: default;
    }

    .sider-hide .ivu-menu-submenu-title span {
        display: none;
    }

    .sider-hide .ivu-menu-item-group-title {
        display: none;
    }

    .sider-hide .ivu-menu-submenu-title-icon {
        display: none;
    }

    .sider-hide .ivu-menu-item span {
        display: none;
    }

    /* 顶部菜单设置 */
    .header {
        z-index: 2;
        box-shadow: 0 2px 2px rgba(0, 0, 0, .05);
        position: fixed;
        top: 0;
        right: 0;
        left: 200px;
        transition: all .2s ease-in-out;
    }
    .sider-hide .header {
        left: 64px;
        transition: all .2s ease-in-out;
    }

    .header-title {
        font-size: 24px;
        white-space: nowrap;
        cursor: default;
    }

    /* 正文 */
    .content {
        z-index: 1;
        border: 1px solid #e8eaec;
        border-radius: 4px;
        padding: 10px;
        margin: 70px 6px;
        background: #fff;
        transition: all .2s ease-in-out;
    }
    .content:hover {
        border-color: #eee;
        box-shadow: 0 1px 6px rgba(0, 0, 0, 0.2);
        transition: all 0.2s ease-in-out;
    }

    .footer {
        z-index: 2;
        box-shadow: 0 -2px 3px rgba(0, 0, 0, 0.05);
        position: fixed!important;
        bottom: 0;
        right: 0;
        left: 200px;
        height: 64px;
        line-height: 64px;
        clear: both;
        padding-right: 10px!important;
        transition: all .2s ease-in-out;
    }
    .sider-hide .footer {
        left: 64px;
        transition: all .2s ease-in-out;
    }

    .data-collect {
        font-size: 24px;
        font-weight: bold;
    }

    .data-collect_not {
        overflow: hidden;
        white-space: nowrap;
    }

    .hidden-nowrap {
        overflow: hidden;
        white-space: nowrap;
    }

    .align-left {
        text-align: left;
    }

    .align-right {
        text-align: right;
    }

    .align-center {
        text-align: center;
    }

    .margin-left8 {
        margin-left: 8px;
    }

    .margin-left16 {
        margin-left: 16px;
    }

    .margin-left24 {
        margin-left: 24px;
    }

    .margin-top16 {
        margin-top: 16px;
    }

    .margin-right8 {
        margin-right: 8px;
    }

    .margin-right16 {
        margin-right: 16px;
    }

    .margin-bottom8 {
        margin-bottom: 8px;
    }

    .margin-bottom16 {
        margin-bottom: 16px;
    }

    .margin-bottom22 {
        margin-bottom: 22px;
    }

    .margin-bottom24 {
        margin-bottom: 24px;
    }

    .margin-bottom32 {
        margin-bottom: 32px;
    }

    .ivu-divider-horizontal {
        margin: 16px 0;
    }

    /*分割线*/
    .split {
        border: 1px solid #e8eaec;
        border-radius: 4px;
        background: #fff;
        transition: all .2s ease-in-out;
    }

    .split:hover {
        border-color: #eee;
        box-shadow: 0 1px 6px rgba(0, 0, 0, .2);
        transition: all .2s ease-in-out;
    }

    .slot-left {
        padding: 16px;
    }

    .slot-right {
        padding: 16px 16px 16px 20px;
    }
</style>