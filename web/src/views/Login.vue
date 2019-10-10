<template>
    <Logon>
        <LogonQR slot="login"></LogonQR>
    </Logon>
</template>
<script>
    import Logon from '../components/Logon'
    import LogonQR from '../components/LogonQR';

    export default {
        name: 'Login',
        components: {Logon, LogonQR}
    }
</script>