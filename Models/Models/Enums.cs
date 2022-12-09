using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public enum gender
    {
        男 = 0,
        女 = 1,
        未知 = 2,
    }

    public enum role
    {
        普通用户 = 0,
        管理员 = 1,
    }

    public enum status
    {
        审核中 = 0,
        未通过 = 1,
        已完成 = 2,
    }

    public enum healthCodeColor
    {
        绿码 = 0,
        黄码 = 1,
        红码 = 2,
        弹窗 = 3,
        其他 = 4,
    }

    public enum travelCardStatus
    {
        正常 = 0,
        异常 = 1,
    }

    public enum healthStasus
    {
        健康 = 0,
        发热 = 1,
        其他 = 2
    }

    public enum userRLStatus
    {
        未登录 = 0,
        登录成功 = 1,
        用户名或密码错误 = 2,
        注册成功 = 3,
        用户名已存在 = 4,
        注册发生异常 = 5
    }

}
