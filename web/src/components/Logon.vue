<template>
    <div class="login">
        <div class="light">
            <div id="cloud1" class="cloud"></div>
            <div id="cloud2" class="cloud"></div>
            <div class="bg">
                <div class="modal">
                    <slot name="login"></slot>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "Logon",
        components: {},

        data() {
            return {
            }
        },

        mounted() {
            let that = this;
            window.onresize = function () {
                // 登录页面才要进行位置计算
                if (that.$route.path !== '/vlogin') return;

                let awid = document.body.clientWidth;
                let ahei = document.body.clientHeight;
                let left1 = awid / 4 * Math.random();
                let top1 = ahei / 4 * Math.random();
                let left2 = awid / 2 + awid / 4 * Math.random();
                let top2 = ahei / 4 * Math.random();
                document.getElementById('cloud1').style.left = left1 + 'px';
                document.getElementById('cloud1').style.top = top1 + 'px';
                document.getElementById('cloud2').style.left = left2 + 'px';
                document.getElementById('cloud2').style.top = top2 + 'px';
            };
            window.onresize();
        },
    }
</script>

<style scoped>
    .login {
        height: 100%;
        background: #1c77ac;
        overflow: hidden;
    }

    .light {
        height: 100%;
        background: url(../assets/login_light.png) no-repeat center center;
        position: relative;
        overflow: hidden;
    }

    .cloud {
        top: 0px;
        left: 0px;
        width: 405px;
        height: 165px;
        position: absolute;
        background: url(../assets/login_cloud.png) no-repeat;
        z-index: 1;
        opacity: 0.5;
    }

    .bg {
        width: 100%;
        height: 100%;
        position: absolute;
        background: url(../assets/login_bg.png) no-repeat center center;
        z-index: 2;
    }

    .modal {
        position: fixed;
        left: 50%;
        top: 50%;
        width: 586px;
        height: 322px;
        margin-left: -293px;
        margin-top: -191px;
        background: url(../assets/login_modal.png);
        border-radius: 5px;
        box-shadow: 2px 2px 5px #666, -2px -2px 5px #666;
    }
</style>