<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Home extends XBASE_Controller
{

    // 首页内容
    public function index()
    {
        $result = Xcon::gets('ci_sessions');
        Xcon::json(Xcon::NO_ERROR, $result);
    }

    // 返回当前页面的状态编号
    public function token()
    {
        $result = Xcon::gets('ci_sessions');
        Xcon::json(Xcon::NO_ERROR, $result);
    }

}
