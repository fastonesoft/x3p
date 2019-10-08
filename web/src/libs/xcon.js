import md5 from 'js-md5'
import axios from 'axios'
import router from '../router'

let base64 = require('js-base64').Base64;

// 日期格式
let dateFormat = function (value, fmt) {
    if (value instanceof Date) {
        let o = {
            'M+': value.getMonth() + 1,
            'd+': value.getDate(),
            'h+': value.getHours(),
            'm+': value.getMinutes(),
            's+': value.getSeconds(),
            'q+': Math.floor((value.getMonth() + 3) / 3),
            'S+': value.getMilliseconds()
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, ('0000' + value.getFullYear()).substr(-RegExp.$1.length));
        }
        for (let k in o) {
            if (new RegExp('(' + k + ')').test(fmt)) {
                fmt = fmt.replace(RegExp.$1, RegExp.$1.length === 1 ? o[k] : ('00' + o[k]).substr(-RegExp.$1.length));
            }
        }
        return fmt;
    } else {
        return value;
    }

};

// 数据分页
let pageData = function (datas, index, size) {
    if (datas.length <= size) {
        return datas.slice(0, size)
    } else {
        let _start = (index - 1) * size;
        let _end = index * size;
        return datas.slice(_start, _end);
    }
};

// 写入、删除本地session
let stateClear = function () {
    sessionStorage.clear();
};
let stateRead = function () {
    let state_string = sessionStorage.getItem("x3p-store");
    if (state_string) {
        let state_json = base64.decode(state_string);
        return JSON.parse(state_json);
    } else {
        return {};
    }
};
let stateWrite = function (state) {
    let state_json = JSON.stringify(state);
    sessionStorage.setItem("x3p-store", base64.encode(state_json))
};

// 数组、对象不空检测
let isNotNull = function (obj) {
    return !!obj && Object.keys(obj).length > 0;
};

// 数组增删改
let arrsDel = function (arrs, key, keyValue) {
    arrs.forEach((item, index) => {
        if (item[key] === keyValue) {
            arrs.splice(index, 1)
        }
    });
    return arrs;
};
let arrsEdit = function (arrs, key, keyValue, value) {
    arrs.forEach((item, index) => {
        if (item[key] === keyValue) {
            arrs.splice(index, 1, value)
        }
    });
    return arrs;
};

// 以下是axios请求内容改造
let axio = axios.create();
let axio_local = axios.create({
    withCredentials: true,
    baseURL: 'http://localhost/',
    headers: {'Content-Type': 'application/x-www-form-urlencoded'}
});
let is_local = location.hostname === 'localhost';
let ajax = is_local ? axio_local : axio;

let gets = function (url) {
    return new Promise(function (resolve, reject) {
        ajax.get(url).then(res => {
            // 输出请求结果，调试用
            is_local && window.console.log(res);
            if (res && res.data && res.data.code) {
                // code != 0 => error
                if (res.data.code !== -1) {
                    reject(res.data.data);
                } else {
                    router.replace('/vlogin');
                }
            } else {
                resolve(res.data.data)
            }
        }).catch(error => {
            // 输出请求结果，调试用
            is_local && window.console.log('出错：调试信息=>' + error);
            reject('数据请求失败')
        })
    })
};
let posts = function (url, data) {
    let param = data || {};
    return new Promise(function (resolve, reject) {
        ajax.post(url, param).then(res => {
            // 输出请求结果，调试用
            is_local && window.console.log(res);
            if (res && res.data && res.data.code) {
                // code != 0 => error
                if (res.data.code !== -1) {
                    reject(res.data.data);
                } else {
                    router.replace('/vlogin');
                }
            } else {
                resolve(res.data.data)
            }
        }).catch(error => {
            // 输出请求结果，调试用
            is_local && window.console.log('出错：调试信息=>' + error);
            // 正常的结果提示
            reject('数据提交失败')
        })
    })
};

let all = axios.all;
let spread = axios.spread;

export default {
    dateFormat, pageData,
    stateClear, stateRead, stateWrite,
    arrsDel, arrsEdit, isNotNull,
    md5, base64,
    gets, posts, all, spread
};