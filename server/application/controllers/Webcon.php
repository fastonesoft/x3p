<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Webcon extends XC_Controller
{

    public function index()
    {
        $param = Xcon::params();

        Xcon::json(Xcon::NO_ERROR, $param);
    }

    public function add()
    {

    }

    public function adds()
    {

    }

    public function edit()
    {

    }
    

}
