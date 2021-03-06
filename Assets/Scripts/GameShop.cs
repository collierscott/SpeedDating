﻿using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Views;
using OnePF;

namespace Assets.Scripts
{
    public class GameShop : Script
    {
        public const string SkuDeluxe = "speeddating.deluxe";
        public const string SkuCredits = "speeddating.credits";
        private OpenIABClient _openIabClient;

        public void Start()
        {
            var options = new Options
            {
                checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2,
                discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2,
                checkInventory = false,
                verifyMode = OptionsVerifyMode.VERIFY_SKIP,
                prefferedStoreNames = new[] { PlanformDependedSettings.StoreName },
                availableStoreNames = new[] { PlanformDependedSettings.StoreName },
                storeKeys = new Dictionary<string, string>
                {
                    { PlanformDependedSettings.StoreName, PlanformDependedSettings.StorePublicKey }
                },
                storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT
            };

            _openIabClient = new OpenIABClient(options);
            _openIabClient.Purchased += Purchased;
            _openIabClient.Restored += Purchased;
            _openIabClient.RestoreCompleted += RestoreCompleted;

            _openIabClient.MapSku(PlanformDependedSettings.StoreName, new Dictionary<string, string>
            {
                { SkuDeluxe, SkuDeluxe },
                { SkuCredits, SkuCredits }
            });
        }

        public void Buy(string sku)
        {
            #if UNITY_EDITOR || UNITY_WEBPLAYER

            Purchased(sku);

            #else

            _openIabClient.PurchaseProduct(sku);

            #endif
        }

        public void Restore()
        {
            _openIabClient.Restore();
        }

        // Handlers

        private void Purchased(Purchase purchase)
        {
            #if UNITY_IPHONE || UNITY_WEBPLAYER

            var verified = true;

            #elif UNITY_ANDROID

            var verified = GooglePlayPurchaseGuard.Verify(purchase.OriginalJson, purchase.Signature, PlanformDependedSettings.StorePublicKeyXml);

            #endif

            if (purchase.Sku == SkuCredits)
            {
                _openIabClient.ConsumeProduct(purchase, true);
            }

            if (verified)
            {
                Purchased(purchase.Sku);
            }
        }

        private void Purchased(string sku)
        {
            switch (sku)
            {
                case SkuDeluxe:
                    Profile.Deluxe = true;
                    break;
                case SkuCredits:
                    Profile.Coins += 10;
                    break;
            }

            Refresh();
            Get<AudioPlayer>().PlaySuccess();
        }

        private static void RestoreCompleted()
        {
        }

        private void Refresh()
        {
            Get<Menu>().Open();
            Get<AudioPlayer>().PlaySuccess();
        }
    }
}