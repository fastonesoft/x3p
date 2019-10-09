<template>
    <div class="login">
        <div class="body">
            <div id="cloud1" class="cloud"></div>
            <div id="cloud2" class="cloud"></div>
            <div class="bg">
                <slot name="passLogin"></slot>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "dev-login",

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

<style>
    .login {
        height: 100%;
        background: #1c77ac;
        overflow: hidden;
    }

    .body {
        width: 100%;
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
        overflow: hidden;
        position: absolute;
        background: url(../assets/login_bg.png) no-repeat center center;
        z-index: 2;
    }

</style>