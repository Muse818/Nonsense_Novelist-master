using System.Collections.Generic;
using UnityEngine;
using System;

///<summary>
///所有技能静态类
///</summary>
public static class AllSkills
{
    #region 词条
    /// <summary>全部名词词条</summary>
    public static List<Type> list_noun = new List<Type>();
    /// <summary>全部形容词词条</summary>
    public static List<Type> list_adj= new List<Type>();
    /// <summary>全部动词词条</summary>
    public static List<Type> list_verb = new List<Type>();
    /// <summary>全部词条</summary>
    public static List<Type> list_all = new List<Type>();

    /// <summary>红楼梦名词词条</summary>
    public static List<Type> hlmList_noun = new List<Type>();
    /// <summary>红楼梦形容词词条</summary>
    public static List<Type> hlmList_adj = new List<Type>();
    /// <summary>红楼梦动词词条</summary>
    public static List<Type> hlmList_verb = new List<Type>();
    /// <summary>红楼梦全部词条</summary>
    public static List<Type> hlmList_all = new List<Type>();
    /// <summary>红楼梦角色卡牌</summary>
    public static List<Type> hlmList_chara=new List<Type>();

    /// <summary>动物园名词词条</summary>
    public static List<Type> animalList_noun = new List<Type>();
    /// <summary>动物园形容词词条</summary>
    public static List<Type> animalList_adj = new List<Type>();
    /// <summary>动物园动词词条</summary>
    public static List<Type> animalList_verb = new List<Type>();
    /// <summary>动物园全部词条</summary>
    public static List<Type> animalList_all = new List<Type>();
    public static List<Type> animalList_chara = new List<Type>();

    /// <summary>仿生人名词词条</summary>
    public static List<Type> humanList_noun = new List<Type>();
    /// <summary>仿生人形容词词条</summary>
    public static List<Type> humanList_adj = new List<Type>();
    /// <summary>仿生人动词词条</summary>
    public static List<Type> humanList_verb = new List<Type>();
    /// <summary>仿生人全部词条</summary>
    public static List<Type> humanList_all = new List<Type>();
    public static List<Type> humanList_chara = new List<Type>();

    /// <summary>水晶能量名词词条</summary>
    public static List<Type> crystalList_noun = new List<Type>();
    /// <summary>水晶能量形容词词条</summary>
    public static List<Type> crystalList_adj = new List<Type>();
    /// <summary>水晶能量动词词条</summary>
    public static List<Type> crystalList_verb = new List<Type>();
    /// <summary>水晶能量全部词条</summary>
    public static List<Type> crystalList_all = new List<Type>();
        public static List<Type> crystalList_chara = new List<Type>();

    /// <summary>莎乐美名词词条</summary>
    public static List<Type> shaLeMeiList_noun = new List<Type>();
    /// <summary>莎乐美形容词词条</summary>
    public static List<Type> shaLeMeiList_adj = new List<Type>();
    /// <summary>莎乐美动词词条</summary>
    public static List<Type> shaLeMeiList_verb = new List<Type>();
    /// <summary>莎乐美全部词条</summary>
    public static List<Type> shaLeMeiList_all = new List<Type>();
    public static List<Type> shaLeMeiList_chara=new List<Type>();

    /// <summary>埃及神话名词词条</summary>
    public static List<Type> aiJiShenHuaList_noun = new List<Type>();
    /// <summary>埃及神话形容词词条</summary>
    public static List<Type> aiJiShenHuaList_adj = new List<Type>();
    /// <summary>埃及神话动词词条</summary>
    public static List<Type> aiJiShenHuaList_verb = new List<Type>();
    /// <summary>埃及神话全部词条</summary>
    public static List<Type> aiJiShenHuaList_all = new List<Type>();
    public static List<Type> aiJiShenHuaList_chara= new List<Type>();

    /// <summary>流行病学名词词条</summary>
    public static List<Type> liuXingBXList_noun = new List<Type>();
    /// <summary>流行病学形容词词条</summary>
    public static List<Type> liuXingBXList_adj = new List<Type>();
    /// <summary>流行病学动词词条</summary>
    public static List<Type> liuXingBXList_verb = new List<Type>();
    /// <summary>流行病学全部词条</summary>
    public static List<Type> liuXingBXList_all = new List<Type>();
    public static List<Type> liuXingBXList_chara=new List<Type>();

    /// <summary>蚂蚁帝国名词词条</summary>
    public static List<Type> maYiDiGuoList_noun = new List<Type>();
    /// <summary>蚂蚁帝国形容词词条</summary>
    public static List<Type> maYiDiGuoList_adj = new List<Type>();
    /// <summary>蚂蚁帝国动词词条</summary>
    public static List<Type> maYiDiGuoList_verb = new List<Type>();
    /// <summary>蚂蚁帝国全部词条</summary>
    public static List<Type> maYiDiGuoList_all = new List<Type>();
    public static List <Type> maYiDiGuoList_chara = new List<Type>();
   
    /// <summary>通用名词词条</summary>
    public static List<Type> commonList_noun = new List<Type>();
    /// <summary>通用形容词词条</summary>
    public static List<Type> commonList_adj = new List<Type>();
    /// <summary>通用动词词条</summary>
    public static List<Type> commonList_verb = new List<Type>();
    /// <summary>通用全部词条</summary>
    public static List<Type> commonList_all = new List<Type>();
    public static List<Type> commonList_chara = new List<Type>();
   
    /// <summary>战斗界面全部词条</summary>
    public static List<Type> combatList_all = new List<Type>();
#endregion

    /// <summary>6个初始词条</summary>
    public static Type[] absWords = new Type[6];

    /// <summary>测试词条</summary>
    public static List<Type> BadBuff = new List<Type>();
    public static List<Type> GoodBuff = new List<Type>();


    /// <summary>测试词条</summary>
    public static List<Type> testList1 = new List<Type>();

    //设定
    /// <summary>平庸库</summary>
    public static List<Type> setting_PingYong = new List<Type>();
    /// <summary>巧思库</summary>
    public static List<Type> setting_QiaoSi = new List<Type>();
    /// <summary>鬼才库</summary>
    public static List<Type> setting_GuiCai = new List<Type>();
    /// <summary>独特库</summary>
    public static List<Type> setting_DuTe = new List<Type>();
    /// <summary>角色标签库</summary>
    public static List<Type> setting_Chara = new List<Type>();

    //卡牌稀有度
    public static List<Type> Rare_1 = new List<Type>();
    public static List<Type> Rare_2 = new List<Type>();
    public static List<Type> Rare_3 = new List<Type>();
    public static List<Type> Rare_4 = new List<Type>();
    /// <summary>
    /// 静态构造函数
    /// </summary>
    static AllSkills()
    {
        #region 词条
        //添加动词词条
        list_verb.AddRange(new Type[] {   typeof(WritePoem) , typeof(BuryFlower),typeof(WanShua),typeof(ShaYu), typeof(FangFuShu),
            typeof(ShenPan),typeof(QiChongShaDance),typeof(Kiss),typeof(TongPinGongZhen),typeof(TuLingCeShi),
             typeof(GunShoot), typeof(MianYiZengQiang),/*typeof(ChanLuan),typeof(XuanZhan),*/typeof(HeartBroken),typeof(Shuai),
            typeof(ToBigger), typeof(BaoZa),typeof(XuanHua)
         //未测试通过：,typeof(WenYiChuanBo) , 
          });
        
        //添加形容词词条W
        list_adj.AddRange(new Type[] {  typeof(ChaFanWuXin),typeof(ShenYouHuanJing),typeof(KeBan),typeof(HunHe),
            typeof(YongJi),  typeof(YouAnQuanGan), typeof(BuXiu),  typeof(FengChan),  typeof(BeiPanDe),typeof(XinShenJiDang),
            typeof(LuanLun),typeof(HunQianMengYing),typeof(QingXi),typeof(JuCaiDe),typeof(FuNengLiangDe),
            typeof(HeWuRan), /*typeof(RenZao),*/ typeof(XiaYuDe),typeof(KeSou),/*typeof(ShenHuanFeiYan)*/typeof(GuoMin),
            typeof(GeLi),typeof(NanYiXiaoMieDe),/*typeof(SheHuiHua), typeof(HunFei),typeof(HaoZhan),*/typeof(FengLi),
            typeof(QuicklyGrowing),typeof(LuoYingBinFen) ,
             typeof(CuZhuang),typeof(JianRuPanShi), typeof(ZhongDu),typeof(YeSheng),
            //未测试通过：
         }) ;
                                                 
        //添加名词词条
        list_noun.AddRange(new Type[] {  typeof(ChaBei),typeof(LengXiangWan),typeof(TongLingBaoyu),
            typeof(ShiWuFengRong),typeof(SheQunFengRong),typeof(RiLunGuaZhui),typeof(herusizhiyan),typeof(XianZhiHead),typeof(LiWu),
            typeof(BaiShuijing) ,typeof(ZiShuiJIng),typeof(HuYanShi) ,typeof(MeiGuiShiYing) ,typeof(Nexus_6Arm),
             typeof(BeiZhiRuDeJiYi),typeof(VolumeProduction) ,typeof(JiShengChong),typeof(EXingZhongLiu),/*typeof(WaiGuGe), 
            typeof(DuXian),*/ typeof(FuTouAxe),  typeof(HouZiDian),typeof(QiGuaiShiXiang),typeof(BoLiGuaZhui),
             //未测试通过：typeof(SheQunFengRong),
        
            //缺少的已补完
        });
        //全部
        list_all.AddRange(list_verb);
        list_all.AddRange(list_adj);
        list_all.AddRange(list_noun);

        #region
        //《红楼梦》添加动词词条
        hlmList_verb.AddRange(new Type[] { typeof(BuryFlower), typeof(WritePoem)});
        //《红楼梦》添加形容词词条
        hlmList_adj.AddRange(new Type[] { typeof(ShenYouHuanJing), typeof(ChaFanWuXin),  });
        //《红楼梦》添加名词词条
        hlmList_noun.AddRange(new Type[] { typeof(TongLingBaoyu), typeof(LengXiangWan), typeof(ChaBei),  });
        //《红楼梦》添加角色词条
        hlmList_chara.AddRange(new Type[] { typeof(LinDaiYu), typeof(WangXiFeng), });
        //《红楼梦》全部词条
        hlmList_all.AddRange(hlmList_chara);
        hlmList_all.AddRange(hlmList_noun);
        hlmList_all.AddRange(hlmList_verb);
        hlmList_all.AddRange(hlmList_adj);

        //动物园添加动词词条
        animalList_verb.AddRange(new Type[] { typeof(WanShua), typeof(ShaYu) });
        //动物园添加形容词词条
        animalList_adj.AddRange(new Type[] {  typeof(YouAnQuanGan) ,typeof(HunHe), typeof(KeBan), typeof(YongJi)});
        //动物园添加名词词条
        animalList_noun.AddRange(new Type[] { typeof(ShiWuFengRong), typeof(SheQunFengRong) });
        //动物园添加角色词条
        animalList_chara.AddRange(new Type[] { typeof(SiYangYuan)/*, typeof(CS_BenJieShiDui), typeof(CS_YiZhiWeiShiQi), typeof(CS_HunYangLong)*/ });
        //动物园全部词条
        animalList_all.AddRange(animalList_chara);
        animalList_all.AddRange(animalList_noun);
        animalList_all.AddRange(animalList_verb);
        animalList_all.AddRange(animalList_adj);

        //埃及神话添加动词词条
        aiJiShenHuaList_verb.AddRange(new Type[] { typeof(ShenPan),typeof(FangFuShu),  });
        //埃及神话添加形容词词条
        aiJiShenHuaList_adj.AddRange(new Type[] { typeof(BuXiu), typeof(FengChan),  typeof(BeiPanDe)});
        //埃及神话添加名词词条
        aiJiShenHuaList_noun.AddRange(new Type[] { typeof(RiLunGuaZhui), typeof(herusizhiyan), });

        aiJiShenHuaList_chara.AddRange(new Type[] { typeof(MuNaiYi),typeof(Anubis)});
        //埃及神话全部词条
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_verb);
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_adj);
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_noun);


        //莎乐美添加动词词条
        shaLeMeiList_verb.AddRange(new Type[] { typeof(QiChongShaDance), typeof(Kiss) });
        //莎乐美添加形容词词条
        shaLeMeiList_adj.AddRange(new Type[] { typeof(XinShenJiDang), typeof(LuanLun), typeof(HunQianMengYing), });
        //莎乐美添加名词词条
        shaLeMeiList_noun.AddRange(new Type[] { typeof(XianZhiHead),typeof(LiWu) });

        shaLeMeiList_chara.AddRange(new Type[] { typeof(ShaLeMei),typeof(ShiLian)});
        //莎乐美全部词条
        shaLeMeiList_all.AddRange(shaLeMeiList_verb);
        shaLeMeiList_all.AddRange(shaLeMeiList_adj);
        shaLeMeiList_all.AddRange(shaLeMeiList_noun);


        //水晶能量添加动词词条
        crystalList_verb.AddRange(new Type[] { typeof(TongPinGongZhen) });
        //水晶能量添加形容词词条
        crystalList_adj.AddRange(new Type[] { typeof(QingXi),typeof(FuNengLiangDe),typeof(JuCaiDe) });
        //水晶能量添加名词词条
        crystalList_noun.AddRange(new Type[] { typeof(BaiShuijing), typeof(ZiShuiJIng), typeof(HuYanShi), typeof(MeiGuiShiYing) });
        //水晶能量全部词条
        crystalList_all.AddRange(crystalList_verb);
        crystalList_all.AddRange(crystalList_adj);
        crystalList_all.AddRange(crystalList_noun);

        //仿生人添加动词词条
        humanList_verb.AddRange(new Type[] { typeof(TuLingCeShi), typeof(GunShoot) });
        //仿生人添加形容词词条
        humanList_adj.AddRange(new Type[] { typeof(HeWuRan), typeof(RenZao),typeof(XiaYuDe) });
        //仿生人添加名词词条
        humanList_noun.AddRange(new Type[] { typeof(Nexus_6Arm), typeof(VolumeProduction), typeof(BeiZhiRuDeJiYi) });


        humanList_chara.AddRange(new Type[] { typeof(DiKaDe), typeof(LongDuanGongSi), });
        //仿生人全部词条
        humanList_all.AddRange(humanList_verb);
        humanList_all.AddRange(humanList_adj);
        humanList_all.AddRange(humanList_noun);

     
        //流行病学添加动词词条
        liuXingBXList_verb.AddRange(new Type[] { /*typeof(WenYiChuanBo),*/typeof(MianYiZengQiang),});
        //流行病学添加形容词词条
        liuXingBXList_adj.AddRange(new Type[] { /*typeof(ShenHuanFeiYan),*/typeof(GuoMin),typeof(GeLi),typeof(NanYiXiaoMieDe)});
        //流行病学添加名词词条
        liuXingBXList_noun.AddRange(new Type[] { typeof(JiShengChong),typeof(EXingZhongLiu),});
        //流行病学全部词条
        liuXingBXList_all.AddRange(liuXingBXList_verb);
        liuXingBXList_all.AddRange(liuXingBXList_adj);
        liuXingBXList_all.AddRange(liuXingBXList_noun);

        //蚂蚁帝国添加动词词条
        maYiDiGuoList_verb.AddRange(new Type[] { typeof(XuanZhan),typeof(ChanLuan), });
        //蚂蚁帝国添加形容词词条
        maYiDiGuoList_adj.AddRange(new Type[] { typeof(SheHuiHua),typeof(HunFei),typeof(HaoZhan),});
        //蚂蚁帝国添加名词词条
        maYiDiGuoList_noun.AddRange(new Type[] { typeof(DuXian), typeof(WaiGuGe),});

        maYiDiGuoList_chara.AddRange(new Type[] { typeof(BeiLuoJi) });
        //蚂蚁帝国全部词条
        maYiDiGuoList_all.AddRange(maYiDiGuoList_verb);
        maYiDiGuoList_all.AddRange(maYiDiGuoList_adj);
        maYiDiGuoList_all.AddRange(maYiDiGuoList_noun);
    
        //通用添加动词词条
        commonList_verb.AddRange(new Type[] { typeof(Shuai),typeof(HeartBroken),typeof(ToBigger),typeof(BaoZa),typeof(XuanHua)});
        //通用添加形容词词条
        commonList_adj.AddRange(new Type[] {typeof(ZhongDu),typeof(FengLi), typeof(QuicklyGrowing), typeof(LuoYingBinFen),typeof(CuZhuang),
            typeof(JianRuPanShi),typeof(YeSheng)});
        //通用添加名词词条
        commonList_noun.AddRange(new Type[] { typeof(FuTouAxe),typeof(HouZiDian),typeof(QiGuaiShiXiang),typeof(BoLiGuaZhui)});

        commonList_chara.AddRange(new Type[] { typeof(Rat)/*,typeof(PianJian),typeof(JingCha)*/ });
        //通用全部词条
        commonList_all.AddRange(commonList_verb);
        commonList_all.AddRange(commonList_adj);
        commonList_all.AddRange(commonList_noun);


        /// <summary>战斗界面全部词条</summary>
        combatList_all.AddRange(shaLeMeiList_all);

        //稀有度1的词条
        Rare_1.AddRange(new Type[] {typeof(ChaBei), typeof(LiWu), typeof(BaiShuijing), typeof(ZiShuiJIng), typeof(BeiZhiRuDeJiYi),
        typeof(VolumeProduction), typeof(EXingZhongLiu), typeof(WaiGuGe), typeof(FuTouAxe), typeof(HouZiDian), typeof(QiGuaiShiXiang),
        typeof(BoLiGuaZhui), 
        //
        typeof(FuShi), typeof(MianYiZengQiang), typeof(ChanLuan), typeof(HeartBroken), typeof(Shuai), typeof(ToBigger),
            typeof(BaoZa), typeof(XuanHua),
        //
        typeof(ChaFanWuXin), typeof(KeBan), typeof(YongJi), typeof(YouAnQuanGan), typeof(FengChan), typeof(BeiPanDe), typeof(HunQianMengYing), 
            typeof(RenZao), typeof(XiaYuDe), typeof(GeLi), typeof(HaoZhan), typeof(FengLi), typeof(QuicklyGrowing), typeof(LuoYingBinFen),
            typeof(CuZhuang), typeof(JianRuPanShi), typeof(YeSheng )
        });

        Rare_2.AddRange(new Type[] {typeof(LengXiangWan), typeof(SheQunFengRong), typeof(HuYanShi), typeof(MeiGuiShiYing), 
            typeof(JiShengChong),
        //
        typeof(ShaYu), typeof(FangFuShu), typeof(Kiss), typeof(GunShoot),
        //
        typeof(ShenYouHuanJing), typeof(HunHe), typeof(BuXiu), typeof(XinShenJiDang), typeof(LuanLun), typeof(QingXi), 
            typeof(JuCaiDe), typeof(FuNengLiangDe), typeof(HeWuRan), typeof(KeSou), typeof(GuoMin), typeof(HunFei), typeof(ZhongDu)
        });

        Rare_3.AddRange(new Type[] {typeof(ShiWuFengRong), typeof(RiLunGuaZhui), typeof(herusizhiyan), typeof(XianZhiHead), typeof(DuXian),
        //
        typeof(XuanZhan), /*typeof(WenYiChuanBo),*/ typeof(TongPinGongZhen), typeof(WanShua),
        //
        typeof(NanYiXiaoMieDe), typeof(SheHuiHua)});

        Rare_4.AddRange(new Type[] { typeof(TongLingBaoyu), typeof(BuryFlower), typeof(ShenPan), typeof(QiChongShaDance),typeof(TuLingCeShi)});


        //手动增加好坏词条
        BadBuff.AddRange(new Type[] { typeof(DianDao),typeof(FuShi ),typeof(Toxic),typeof(Ill),typeof(XuRuo),/*typeof(PoJia),*/
            typeof(MuNe),typeof(YiLuan),typeof(HanLen),/*typeof(ChiHuan),typeof(LengMo),*/typeof(QingMi),typeof(Dizzy),typeof(Upset)});
        GoodBuff.AddRange(new Type[] { typeof(QiWu), typeof(ReLife),typeof(ChaoFeng),typeof(HuaBan),
            typeof(GongZhen),typeof(GaiZao),typeof(ChongLuan),typeof(HuoRe),
        typeof(ShiQing),typeof(HeShan),typeof(RuiLi),typeof(JianShi),/*typeof(JiSu),*/typeof(ZaiSheng),typeof(KangFen),});
     #endregion

        ///<summary>测试词条1</summary>
        testList1.AddRange(new Type[] {  typeof(SheQunFengRong),typeof(ChanLuan),typeof(ShiWuFengRong) });

        #endregion

        //设定
        setting_PingYong.AddRange(new Type[] { typeof(HaiTangChunShui), typeof(JuLingQiShu), typeof(ShuoShuShuoShu) , typeof(ShengSiZaiWo ), typeof(JingXinHeHu) 
        ,typeof(DongWuZhuanJia),typeof(XiXiXiangTong),typeof(MiaoYuLianZhu),typeof(YiShiErNiao),typeof(QianXianMingLiao),typeof(YiEChuanE),typeof(ShiDaLiChen),typeof(ChaoPinChongJi)
        ,typeof(YiYiDaiLao),typeof(JiuHouYuXing),typeof(YinJiuLeShen),typeof(CaiSIQuanYong),typeof(ZangHuaJuHun),typeof(LuoHuaShiJie),typeof(ShengCunJiQiao),typeof(PiMaoZengHou)
        ,typeof(DongWuZhuanJia),typeof(XiuYangShengXi ),typeof(AiZhuanJiuJue),typeof(ZhuRenMingLing),typeof(BingRuGaoHuang),typeof(LuoJingXiaShi),typeof(ZhenFuPingWen),typeof(FuFuDeZheng)
        ,typeof(JianDuanYiLiao),typeof(QiangZhiFanLan),typeof(QiaoGuShiSui),typeof(ShouShuZhunBei),typeof(BingFaZheng),typeof(MianYiXiTong),typeof(YiQunZhuQiang),typeof(YiMingDiMing)
        ,});
        setting_QiaoSi.AddRange(new Type[] {typeof(JinYuManTang),typeof(KeJuanZaShui),typeof(SuoDuoMaZhiNv),typeof(YaoHaiZhenCe),typeof(JiNiTaiMei) ,
        typeof(XueShangJiaShuang),typeof(MouDingHouDong),typeof(NengLiangHuanChong),typeof(FengChiDianChe),typeof(XianShouHengChang),typeof(JinJiJiuYuan),typeof(YuanQuGuiHua),typeof(ShiPuYouHua)
        ,typeof(TongShengGOngSi),typeof(YiChanZhuanRang),typeof(WuZhiAiQuan),typeof(QingMiZhiWu),typeof(KuMuFengChun),typeof(QuanBoDuan),typeof(HuaXueJiLe),typeof(XuNiShengMingLi),typeof(ManXingYanZheng)
        ,typeof(YinKeXiaoYing),typeof(DiXiXinHao)});
        setting_GuiCai.AddRange(new Type[] { typeof(ChengShengZhuiJi),typeof(MeiFeiSeWu), typeof(DongWuLeYuan), typeof(CanHeYuSheng), typeof(DongWuLeYuan), typeof(QunQingJIFen), typeof(AiYiFanLan)
        , typeof(CiShengBo), typeof(ZhiHuiHeXin), typeof(FangXueLiaoFa), typeof(ChongFenLiYong)});
        setting_DuTe.AddRange(new Type[] { typeof(RenXingShangCun), typeof(ShenJingWenLuan) });

        setting_Chara.AddRange(new Type[] { typeof(ShuoShuShuoShu), typeof(HaiTangChunShui), typeof(CanHeYuSheng), typeof(JinYuManTang), typeof(DongWuZhuanJia)
        , typeof(ShiPuYouHua), typeof(JuLingQiShu), typeof(ShengSiZaiWo), typeof(SuoDuoMaZhiNv), typeof(LuoJingXiaShi), typeof(QiaoGuShiSui), typeof(KeJuanZaShui)
        , typeof(YaoHaiZhenCe), typeof(JingXinHeHu), typeof(JiNiTaiMei)});
    }

    /// <summary>
    /// 【弹射版本】生成随机词条(包含名词+动词+形容词)
    /// </summary>
    /// <returns></returns>
    public static Type CreateSkillWord()
    {
        #region
        

        #endregion

        //测试用，指定某种卡牌↓

        int _number = UnityEngine.Random.Range(0, testList1.Count);

        return testList1[_number];



        ////正式

        //int number = UnityEngine.Random.Range(0, GameMgr.instance.GetNowList().Count);
        //while (testList3.Contains(GameMgr.instance.GetNowList()[number]))
        //{
        //    number = UnityEngine.Random.Range(0, GameMgr.instance.GetNowList().Count);
        //}

        //return GameMgr.instance.GetNowList()[number];
    }
    public static Type AllAdjWords(int i)
    {
        if (i > list_adj.Count) return list_adj[0];
        return list_adj[i];
    }
    public static Type AllNounWords(int i)
    {
        if (i > list_noun.Count) return list_noun[0];
        return list_noun[i];
    }
    public static Type AllVerbWords(int i)
    {
        if (i > list_verb.Count) return list_verb[0];
        return list_verb[i];
    }
    
    public static Type RandomPY()
    {
        int _number = UnityEngine.Random.Range(0, setting_PingYong.Count);

        return setting_PingYong[_number];
    }
    public static Type RandomQS()
    {
        int _number = UnityEngine.Random.Range(0, setting_QiaoSi.Count);

        return setting_QiaoSi[_number];
    }
    public static Type RandomGC()
    {
        int _number = UnityEngine.Random.Range(0, setting_GuiCai.Count);

        return setting_GuiCai[_number];
    }
    public static Type RandomDT()
    {
        int _number = UnityEngine.Random.Range(0, setting_DuTe.Count);

        return setting_DuTe[_number];
    }
    
    public static Type RandomChara()
    {
        int _number = UnityEngine.Random.Range(0, setting_Chara.Count);

        return setting_Chara[_number];
    }

}
