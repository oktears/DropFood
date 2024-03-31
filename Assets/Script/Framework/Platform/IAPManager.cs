//
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Purchasing;
//
// namespace Chengzi
// {
//
//     /// <summary>
//     /// 商品
//     /// </summary>
//     [Serializable]
//     public class Product
//     {
//         public Product(PayConstant.ProductId id, ProductType type)
//         {
//             this._id = id;
//             this._type = type;
//         }
//
//         public PayConstant.ProductId _id;
//         public ProductType _type;
//     }
//
//     /// <summary>
//     /// IAP支付管理器
//     /// </summary>
//     public class IAPManager : MonoSingleton<IAPManager>, IStoreListener
//     {
//         public List<Product> products = new List<Product>();
//         public string publicKey;
//         ConfigurationBuilder builder;
//         private IStoreController m_Controller;
//         private IAppleExtensions m_AppleExtensions;
//         private static bool isInited = false;
//         private bool isInitFailed = false;
//
//         public const string GOOGLE_ID_REMOVE_AD = "com.cc.dropfood_pid01";
//         public const string GOOGLE_ID_GOLD_SMALL = "com.cc.dropfood_pid02";
//         public const string GOOGLE_ID_GOLD_MID = "com.cc.dropfood_pid03";
//         public const string GOOGLE_ID_GOLD_BIG = "com.cc.dropfood_pid04";
//         public const string GOOGLE_ID_GOLD_HUGE = "com.cc.dropfood_pid05";
//         public const string GOOGLE_ID_GOLD_UNLIMIT = "com.cc.dropfood_pid06";
//
//
//         void Awake()
//         {
//         }
//
//         public void init()
//         {
//             initProducts();
//             InitPurchase();
//         }
//
//         /// <summary>
//         /// 初始化商品
//         /// </summary>
//         private void initProducts()
//         {
//             //去广告
//             Product removeAD = new Product(PayConstant.ProductId.REMOVE_AD, ProductType.NonConsumable);
//             Product gold1 = new Product(PayConstant.ProductId.GOLD_SMALL, ProductType.Consumable);
//             Product gold2 = new Product(PayConstant.ProductId.GOLD_MID, ProductType.Consumable);
//             Product gold3 = new Product(PayConstant.ProductId.GOLD_BIG, ProductType.Consumable);
//             Product gold4 = new Product(PayConstant.ProductId.GOLD_HUGE, ProductType.Consumable);
//             Product gold5 = new Product(PayConstant.ProductId.GOLD_UNLIMIT, ProductType.Consumable);
//
//             products.Add(removeAD);
//             products.Add(gold1);
//             products.Add(gold2);
//             products.Add(gold3);
//             products.Add(gold4);
//             products.Add(gold5);
//         }
//
//
//         /// <summary>
//         /// 初始化
//         /// </summary>
//         void InitPurchase()
//         {
//             if (!isInited)
//             {
//                 Debug.Log("初始化");
//                 var module = StandardPurchasingModule.Instance();
//                 builder = ConfigurationBuilder.Instance(module);
//                 for (int i = 0; i < products.Count; i++)
//                 {
//                     PayInfoData payInfoData = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == products[i]._id);
//                     builder.AddProduct(payInfoData._googlePlayId, products[i]._type);
//                 }
//                 UnityPurchasing.Initialize(this, builder);
//             }
//         }
//
//         /// <summary>
//         /// 初始化成功
//         /// </summary>
//         /// <param name="controller">Controller.</param>
//         /// <param name="extensions">Extensions.</param>
//         public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//         {
//             //Debug.Log("初始化成功");
//             m_Controller = controller;
//             m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
//             m_AppleExtensions.RegisterPurchaseDeferredListener(OnDeferred);
//             isInited = true;
//         }
//
//         /// <summary>
//         /// iOS ⽹网络延迟错误
//         /// </summary>
//         /// <param name="item">Item.</param>
//         private void OnDeferred(UnityEngine.Purchasing.Product item)
//         {
//             //Debug.Log("⽹网络连接不不稳");
//             PlatformManager.Instance.runOnUIThread().showToast("Network Instability.", false);
//             CommonViewManager.Instance.showWaitView(false);
//         }
//
//         /// <summary>
//         /// 初始化失败
//         /// </summary>
//         /// <param name="error">Error.</param>
//         public void OnInitializeFailed(InitializationFailureReason error)
//         {
//             isInitFailed = true;
//             //Debug.Log("初始化失败");
//             Debug.Log("IAPInitializeFailed!!!" + "Reason：" + error);
//             CommonViewManager.Instance.showWaitView(false);
//         }
//
//         /// <summary>
//         /// 恢复购买
//         /// </summary>
//         public void restorePurchases()
//         {
//
//             CommonViewManager.Instance.showWaitView(true, "Restoring...");
//             vp_Timer.In(5.0f, new vp_Timer.Callback(() =>
//             {
//                 CommonViewManager.Instance.showWaitView(false);
//             }));
//
//
//             InitPurchase();
//             StartCoroutine("InitAndRestore");
//         }
//
//         IEnumerator InitAndRestore()
//         {
//             if (isInitFailed || !isInited)
//             {
//                 //初始化失败
//                 StopCoroutine("InitAndRestore");
//             }
//             yield return new WaitUntil(() =>
//             {
//                 return m_Controller != null && m_AppleExtensions != null;
//             });
//             m_AppleExtensions.RestoreTransactions((result) =>
//             {
//                 // The first phase of restoration. If no more responses are received on ProcessPurchase then
//                 // no purchases are available to be restored.
//                 Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//
//                 if (result)
//                 {
//                     //产品已经restore，不不过官⽅方的解释是恢复过程成功了了，并不不代表所购买的物品都恢复了了
//                     //BusinessManager.Instance._payBiz.paySuccess(PayConstant.ProductId.REMOVE_AD);
//                     PlatformManager.Instance.runOnUIThread().showToast("Restore Success.", false);
//                 }
//                 else
//                 {
//                     // 恢复失败
//                     PlatformManager.Instance.runOnUIThread().showToast("Restore Fail.", false);
//                 }
//
//                 CommonViewManager.Instance.showWaitView(false);
//                 StopCoroutine("InitAndRestore");
//             });
//         }
//
//         /// <summary>
//         /// 按钮点击  也可以重写为传入产品ID  此处是序列号
//         /// </summary>
//         /// <param name="index">Index.</param>
//         public void pay(PayConstant.ProductId id)
//         {
//
//             PayInfoData payInfoData = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == id);
//
//             //Firebase.Analytics.FirebaseAnalytics.LogEvent(
//             //    Firebase.Analytics.FirebaseAnalytics.EventPurchase,
//             //    Firebase.Analytics.FirebaseAnalytics.ParameterPromotionName,
//             //    payInfoData._name);
//
//             CommonViewManager.Instance.showWaitView(true, "Wating...");
//             vp_Timer.In(5.0f, new vp_Timer.Callback(() =>
//             {
//                 CommonViewManager.Instance.showWaitView(false);
//             }));
//
//
//             InitPurchase();
//             StartCoroutine("InitAndPurchase", id);
//         }
//
//         IEnumerator InitAndPurchase(PayConstant.ProductId id)
//         {
//             Debug.Log("正在发起支付，商品Id=" + id);
//
//
//             if (isInitFailed || !isInited)
//             {
//                 //初始化失败
//                 StopCoroutine("InitAndPurchase");
//             }
//             yield return new WaitUntil(() =>
//             {
//                 return m_Controller != null && m_AppleExtensions != null;
//             });
//
//             PayInfoData payInfoData = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == id);
//             UnityEngine.Purchasing.Product product = m_Controller.products.WithID(payInfoData._googlePlayId);
//             if (product != null && product.availableToPurchase)
//             {
//                 Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//                 m_Controller.InitiatePurchase(product);
//             }
//             else
//             {
//                 Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//             }
//
//             StopCoroutine("InitAndPurchase");
//         }
//
//         /// <summary>
//         /// 购买成功回调
//         /// </summary>
//         /// <returns>The purchase.</returns>
//         /// <param name="e">E.</param>
//         public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//         {
//             //Debug.Log("购买成功回调");
//             //使⽤用id判断是否是当前购买的产品，我这⾥里里只有⼀一个产品，所以就是products[0]
//             //PayInfoData payInfoData = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == id);
//
//             PayConstant.ProductId productId = PayConstant.ProductId.REMOVE_AD;
//             switch (e.purchasedProduct.definition.id)
//             {
//                 case GOOGLE_ID_REMOVE_AD:
//                     productId = PayConstant.ProductId.REMOVE_AD;
//                     break;
//                 case GOOGLE_ID_GOLD_SMALL:
//                     productId = PayConstant.ProductId.GOLD_SMALL;
//                     break;
//                 case GOOGLE_ID_GOLD_MID:
//                     productId = PayConstant.ProductId.GOLD_MID;
//                     break;
//                 case GOOGLE_ID_GOLD_BIG:
//                     productId = PayConstant.ProductId.GOLD_BIG;
//                     break;
//                 case GOOGLE_ID_GOLD_HUGE:
//                     productId = PayConstant.ProductId.GOLD_HUGE;
//                     break;
//                 case GOOGLE_ID_GOLD_UNLIMIT:
//                     productId = PayConstant.ProductId.GOLD_UNLIMIT;
//                     break;
//                 default:
//                     break;
//             }
//
//             if (productId == PayConstant.ProductId.REMOVE_AD)
//             {
//                 //内购恢复逻辑
//                 if (EntityManager.Instance._userEntity._isRemovedAD)
//                 {
//                     //已经恢复过了，无需再次恢复
//                     return PurchaseProcessingResult.Complete;
//                 }
//             }
//
//             BusinessManager.Instance._payBiz.paySuccess(productId);
//
//             PlatformManager.Instance.runOnUIThread().showToast("Purchase Success.", false);
//             CommonViewManager.Instance.showWaitView(false);
//
//             //此处可以对订单进行验证，因为我们项目吧验证放到服务器了，所以我们可以把购买成功后的凭证发给服务器去验证
//             //这个购买验证码特别特别长，验证分为沙盒验证和真实验证 就不赘述了
//             return PurchaseProcessingResult.Complete;
//         }
//
//         /// <summary>
//         /// 购买失败
//         /// </summary>
//         /// <param name="i"></param>
//         /// <param name="p"></param>
//         public void OnPurchaseFailed(UnityEngine.Purchasing.Product i, PurchaseFailureReason p)
//         {
//             //购买失败的逻辑
//             //Debug.Log("购买失败的逻辑");
//             PlatformManager.Instance.runOnUIThread().showToast("Purchase Failed.", false);
//             CommonViewManager.Instance.showWaitView(false);
//         }
//
//     }
// }