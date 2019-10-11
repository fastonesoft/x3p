<template>
    <Logon>
        <LogonUser slot="login"></LogonUser>
    </Logon>
</template>
<script>
    import Logon from '../components/Logon'
    import LogonQR from '../components/LogonQR';
    import LogonUser from '../components/LogonUser';

    export default {
        name: 'Login',
        components: {Logon, LogonQR, LogonUser}
    }
</script>