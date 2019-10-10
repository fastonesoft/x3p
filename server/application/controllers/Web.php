<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Web extends XBASE_Controller
{

    // 首页内容
    public function index()
    {
        $result = Xcon::tables_filter('sessi');
        Xcon::json(Xcon::NO_ERROR, $result);
    }

    public function login()
    {
        $result = [];
        Xcon::json(Xcon::NO_ERROR, $result);
    }

    public function token()
    {
        $result = md5(session_id());
        Xcon::json(Xcon::NO_ERROR, $result);
    }

    // 退出
    public function logout()
    {
        Xcon::sess_destroy();
        Xcon::json(Xcon::NO_ERROR, 1);
    }

}
