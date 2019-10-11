<template>
    <dev-article>
        这是首页
        <Button @click="conClick">请求测试</Button>
    </dev-article>
</template>

<script>
    import $ from '../libs/xcon'

    export default {
        name: 'home',
        components: {},
        methods: {
            conClick() {
                $.posts('/webcon/', {name: 'role', action: 'add', param: {id: true, uid: 'asdfasdff234234'}})
                    .then(res=>{
                        window.console.log(res);
                    })
                    .catch(error=>{
                        this.$Message.info(error)
                    })
            }
        }
    }
</script>
