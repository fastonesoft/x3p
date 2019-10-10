<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Webuser extends XC_Controller
{

    public function index()
    {
        $result = 1;
        Xcon::json(Xcon::NO_ERROR, $result);
    }

    public function group()
    {
        Xcon::loginCheck(function ($userinfor) {
            $result = Xcon::getsBy('xvGroup', null, 'id');
            Xcon::json(Xcon::NO_ERROR, $result);
        });
    }

}
