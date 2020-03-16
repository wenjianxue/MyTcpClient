********************
2.4.0 至 2.5.0 迁移指南
********************

欢迎阅读 2.4.0-2.5.0 迁移指南。在此版本中，我们增加了许多功能并修复了一些BUG。在这个指南中，我们将介绍从以前版本迁移现有代码时最重要的变化：

    * IGGSDK 初始化流程改变
    * AppConf load 接口改变
    * 合规（防沉迷）接入流程变化
    * 游戏协议接入流程变化
    * 规范化客服的 Ticket Service 相关概念，将 CRM 更名为 TSNative
    * com.igg.sdk.payment 下部分包名变化

IGGSDK 初始化流程改变
==============

将获取 AppConfig 合入 IGGSDK 初始化流程 。让游戏初始化流程更加紧凑，接入更加简单。

.. code-block:: java

    .....
    // IGGSDK 初始化
    IGGSDK.sharedInstance().initialize("server_config", IGGSDKInitFinishedListener listener);
    // 不需要再调用 IGGAppConfigService.load 方法加载配置
    //new IGGAppConfigService().load("server_config", IGGAppConfigService.AppConfigListener listener);
    

IGGSDKInitFinishedListener 接口：

.. code-block:: java

    public interface IGGSDKInitFinishedListener {
        /**
         * 该回调返回的是从服务端拿到的appconfig，所以可能返回空（请求失败），也可能不为空（请求成功）。
         * @param primaryConfig
         */
        void onInitialized(IGGAppConfig primaryConfig);

        /**
         *  该回调返回的是从本地拿到的appconfig，会包含这份缓存的时间戳，用于研发校验这份数据的有效性，如果时间太长可以不用这份appconfig。
         * @param primaryConfig
         */
        void onFailback(IGGAppConfigBackup primaryConfig);
    }   

AppConf load 接口改变
==============

* IGGAppConfigService 不在承担主配置（server_config）的加载，主配置的加载改为 IGGSDK 初始化时进行，详情请见：IGGSDK 初始化流程改变。
* IGGAppConfigService load 接口细分为 onAppConfigLoadFinished 和 onAppConfigLoadFailed，分别对应从服务器加载成功和服务器加载失败情况。

.. code-block:: java
    new IGGAppConfigService().load("game_define_config", new IGGAppConfigService.AppConfigListener() {
            /**
            * 从服务器获取 AppConf 成功回调
            *
            * @param appconfig 从服务器获取的 AppConf 配置
            * @param serverTime 服务器时间
            */
            @Override
            public void onAppConfigLoadFinished(IGGAppConfig appconfig, IGGEasternStandardTime serverTime) {
                handle(appconfig, serverTime);
            }

            /**
            * 从服务器获取 AppConf 失败回调
            *
            * @param appConfigBackup 从本地缓存获取的 AppConf 配置，等于 null 时表示本地没有缓存
            * @param currentTime 手机本地时间
            * @param ex 从服务器获取时出现的异常（存在本地缓存时，仍会返回 ex）
            */
            @Override
            public void onAppConfigLoadFailed(IGGAppConfigBackup appConfigBackup, IGGEasternStandardTime currentTime, @NonNull IGGException ex) {
                if (null != appConfigBackup) {
                    //从本地缓存加载的 AppConf，可以根据 appConfigBackup.backupsTimeStamp 判断此缓存是否失效
                    handle(appConfigBackup.appConf, currentTime);
                } else {
                    //使用游戏默认预置的配置进入游戏
                }
                if (!ex.isNone()) {
                    //打印错误
                    Log.d(TAG, "从网络加载 AppConf 失败。");
                    Toast.makeText(IndexActivity.this, "从网络加载 AppConf 失败。", Toast.LENGTH_LONG).show();
                }
            }
    }


合规（防沉迷）接入流程变化
==============

* 新增游客的合规限制。调整了 IGGCompliance 类 check 方法的回调，在游客模式的回调中新增了合规的限制，方便游戏针对游客身份的玩家进行游戏时长以及商品购买的限制。
* 细分购买失败的情况。在购买限制中移除超出配额限购，将之细分为单次购买超出配额限购与本月购买超出配额限购，方便游戏对不同返回进行不同的处理。

IGGSDK Android 2.4.0 合规（防沉迷）接口：

.. code-block:: java

    interface IGGComplianceClearListener {
        /**
         * 防沉迷关闭回调
         */
        fun onPostponing(status: IGGComplianceStatus)

        /**
         * 防沉迷开启：未认证
         */
        fun onUnverified(status: IGGComplianceStatus)

        /**
         * 防沉迷开启：成年人
         */
        fun onAdult(status: IGGComplianceStatus)

        /**
         * 防沉迷开启：未成年人
         */
        fun onFoundMinor(status: IGGComplianceStatus, restrictions: IGGComplianceRestrictions)
        fun onError(ex: IGGException)
    }

**IGGSDK Android 2.5.0 合规（防沉迷）新接口：** 

.. code-block:: java

    interface IGGComplianceClearListener {
        /**
        * 合规无限制回调
        */
        fun onPostponing(status: IGGComplianceStatus)

        /**
        * 游客模式（未认证）的合规限制【此次版本的修改部分】
        */
        fun onGuest(status: IGGComplianceStatus, restrictions: IGGComplianceRestrictions?)

        /**
        * 成年人
        */
        fun onAdult(status: IGGComplianceStatus)

        /**
        * 未成年人的合规限制
        */
        fun onMinor(status: IGGComplianceStatus, restrictions: IGGComplianceRestrictions?)
        fun onError(ex: IGGException)
    }

**IGGSDK Android 2.5.0 合规（防沉迷）购买限制移除超出配额限购，将之细分为单次购买超出配额限购与本月购买超出配额限购：**

.. code-block:: java

    public interface IGGPayResultCode {
        int IGGPaymentPurchaseSuccess = 1;
        int IGGPaymentPurchaseCancel = 2;
        int IGGPaymentPurchaseFailed = -1;
        @Deprecated
        int IGGPaymentErrorPurchaseLimitation = -2;
        int IGGPaymentErrorRestrictionCheckingFailed = -5;
        int IGGPaymentErrorPurchaseLimitationForUser = -6;
        //int IGGPaymentErrorPurchaseLimitationForRunOutOfQuota = -7;// 超出配额限购（防沉迷限购的返回处理）【此次版本的修改部分，移除此项】
        int IGGPaymentErrorPurchaseLimitationForDevice = -8;
        int IGGPaymentErrorPurchaseLimitationForRunOutOfSingleQuota = -9;// 超出配额限购(单次)【此次版本的修改部分】
        int IGGPaymentErrorPurchaseLimitationForRunOutOfMonthQuota = -10;// 超出配额限购(本月)【此次版本的修改部分】
    }

**注意事项：**

* 根据大陆地区防沉迷的要求，游客玩家受如下限制：消费限制。无法在游戏中进行充值；游戏时长限制 。可以享受 1 个小时的游客体验模式。

* Wiki `《手持游戏应用配置:合规配置规范》 <http://wiki.skyunion.net/index.php?title=%E6%89%8B%E6%8C%81%E6%B8%B8%E6%88%8F%E5%BA%94%E7%94%A8%E9%85%8D%E7%BD%AE:%E5%90%88%E8%A7%84%E9%85%8D%E7%BD%AE%E8%A7%84%E8%8C%83>`_ 中新增游客玩家相关的限制。

游戏协议接入流程变化
==============
* 创建 IGGAgreementSigning 的方式发生变化。在 IGGSDK 初始化完成后，调用 IGGSDK.sharedInstance().getAgreementSigning() 获得游戏协议接入的门面类。

* 新增请求同意协议弹窗出现点接口，废弃请求用户协议状态接口（requestStatus）。调用不同的协议弹窗出现点接口，可以根据后台配置动态控制游戏协议弹窗出现的时机，使游戏协议弹窗机制更加灵活。

* 新增“撤销同意协议”。为满足游戏允许用户撤销已同意游戏协议的需求新增接口。

创建 IGGAgreementSigning 的方式：

.. code-block:: java

    //在 IGGSDK 初始化完成后调用
    IGGSDK.sharedInstance().getAgreementSigning();

新增请求同意协议弹窗出现点接口：

.. code-block:: java

    IGGAgreementSigning agreementSigning = IGGSDK.sharedInstance().getAgreementSigning();
    //agreementSigning.requestStatus()//废弃请求用户协议状态接口，使用 informAsap 和 informKindly 接口替代
    //请求同意协议弹窗出现点（Asap 类型）;
    agreementSigning.getAgreementSigningController().informAsap(listener);
    ......
    //请求同意协议弹窗出现点（Kindly 类型）
    agreementSigning.getAgreementSigningController().informKindly(listener2);

新增“撤销同意协议”:

.. code-block:: java

    IGGAgreementSigning agreementSigning = IGGSDK.sharedInstance().getAgreementSigning();
    //获取 “撤销同意协议” 功能的接口类 IGGAgreementTerminationController
    IGGAgreementTerminationController terminationController = agreementSigning.getAgreementTerminationController();
    //请求游戏签署的协议列表和“撤销同意协议”弹窗上显示的文字
    terminationController.requestAssignedAgreements(listener);
    ......
    //用户“撤销同意协议”弹窗上点击“同意”，调用 terminate 接口 
    terminationController.terminate(listener2);


规范化客服的 Ticket Service 相关概念，将 CRM 更名为 TSNative
==============

* 依赖库 CRM.aar 更名为 TSNative.aar。

* IGGCRM 更名为 IGGTSNative。其他类或接口中含有 CRM，均改为 TSNative。

* AndroidManifest.xml 中的 “游戏包名 +.crm.fileProvider” 改为 “游戏包名 + .tsnaive.fileprovider”


com.igg.sdk.payment 下部分包名变化
==============

随着 IGGSDK 集成的第三方支付增多，我们重构了 com.igg.sdk.payment 下的部分包名。

新旧包名对比如下：

==============================  ==============================
2.4.0                            2.5.0  
==============================  ==============================
com.igg.sdk.payment.google.*    com.igg.sdk.payment.flow.*
==============================  ==============================

类的位置发生变化：

============================================================  ============================================================
2.4.0                                                          2.5.0  
============================================================  ============================================================
com.igg.sdk.payment.google.IGGSubscriptionStateListener         com.igg.sdk.payment.flow.listener.IGGSubscriptionStateListener
============================================================  ============================================================
