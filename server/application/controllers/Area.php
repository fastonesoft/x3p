<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Area extends XC_Controller
{

    public function index()
    {
        Xcon::loginCheck(function ($userinfor) {
            $datas = Xcon::gets('xvAreaTown');
            $areas = Xcon::gets('xvArea');

            Xcon::json(Xcon::NO_ERROR, compact('datas', 'areas'));
        });
    }

    public function find()
    {
        Xcon::loginCheck(function ($userinfor) {
            $params = Xcon::params();
            $up_id = Xcon::array_key($params, 'area_id');

            $result = Xcon::getsBy('xvAreaTown', compact('up_id'));

            Xcon::json(Xcon::NO_ERROR, $result);
        });
    }

    public function add()
    {
        Xcon::loginCheck(function ($userinfor) {
            $params = Xcon::params();
            $params['uid'] = Xcon::uid();

            // 重复编号检测
            Xcon::existById('xcArea', Xcon::array_key($params, 'id'));
            // 重复名称检测
            $name = Xcon::array_key($params, 'name');
            Xcon::existBy('xcArea', compact('name'), '“名称”重复');

            Xcon::add('xcArea', $params);
            $result = Xcon::getByUid('xcArea', $params['uid']);

            Xcon::json(Xcon::NO_ERROR, $result);
        });
    }

    public function addtown()
    {
        Xcon::loginCheck(function ($userinfor) {
            $params = Xcon::params();
            $id = Xcon::array_key($params, 'id');
            $name = Xcon::array_key($params, 'name');

            // 重复编号检测
            Xcon::existById('xcArea', $id);
            // 重复名称检测
            Xcon::existBy('xcArea', compact('name'), '“名称”重复');

            $uid = Xcon::uid();
            Xcon::add('xcArea', compact('id', 'uid', 'name'));
            $result = Xcon::getByUid('xvAreaTown', $uid);

            Xcon::json(Xcon::NO_ERROR, $result);
        });
    }

    public function edit()
    {
        Xcon::loginCheck(function ($userinfor) {
            $params = Xcon::params();
            $uid = Xcon::array_key($params, 'uid');
            $name = Xcon::array_key($params, 'name');

            Xcon::setByUid('xcArea', compact('name'), $uid);
            $result = Xcon::getByUid('xvAreaTown', $uid);

            Xcon::json(Xcon::NO_ERROR, $result);
        });
    }

    public function del()
    {
        Xcon::loginCheck(function ($userinfor) {
            $params = Xcon::params();
            // 删除之前要确认一下
            $uid = Xcon::array_key($params, 'uid');
            $area = Xcon::checkByUid('xcArea', $uid);
            $area_id = $area->id;

            // 检测分组列表
            Xcon::existBy('xcData', compact('area_id'), '地区编号已存在标的清单');

            // 删除
            $result = Xcon::delByUid('xcArea', $uid);

            Xcon::json(Xcon::NO_ERROR, $result);
        });
    }

}
