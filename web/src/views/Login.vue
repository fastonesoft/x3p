<template>
    <Logon>
        <LogonUser slot="login" v-if="type!=='qr'"></LogonUser>
        <LogonQR slot="login" v-if="type==='qr'"></LogonQR>
    </Logon>
</template>
<script>
    import Logon from '../components/Logon'
    import LogonQR from '../components/LogonQR';
    import LogonUser from '../components/LogonUser';

    export default {
        name: 'Login',
        data() {
            return {
                type: 'qr',
            }
        },
        components: {Logon, LogonQR, LogonUser}
    }
</script>