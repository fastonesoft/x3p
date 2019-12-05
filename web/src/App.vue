<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>

<script>
    export default {
        name: 'app',
        components: {}
    }
</script>

<style>
    html, body {
        height: 100%;
        min-height: 300px;
    }

    #app {
        height: 100%;
        min-height: 300px;
    }
</style>
