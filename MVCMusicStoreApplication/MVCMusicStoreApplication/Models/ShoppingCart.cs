﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMusicStoreApplication.Models {
    public class ShoppingCart {

        public string ShoppingCartId;

        private MVCMusicStoreDB db = new MVCMusicStoreDB();

        public static ShoppingCart GetCart(HttpContextBase context) {

            ShoppingCart cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        private string GetCartId(HttpContextBase context) {
            //look at session state
            const string CartSessionId = "CartId";
            string cartId;

            //if exists
            if(context.Session[CartSessionId] == null) {
                cartId = Guid.NewGuid().ToString();
                //save to session date
                context.Session[CartSessionId] = cartId;
            } 
            else {
                //if not, return new cart id
                cartId = context.Session[CartSessionId].ToString();
            }
            //return cart id
            return cartId;
        }

        public List<Cart> GetCartItems() {
           return db.Carts.Where(c => c.CartId == this.ShoppingCartId).ToList();
        }

        public decimal GetCartTotal() {
            decimal? total = (from cartItem in db.Carts
                        where cartItem.CartId == this.ShoppingCartId
                        select cartItem.AlbumSelected.Price* (int?)cartItem.Count).Sum();

            return total ?? decimal.Zero;
        }

        public void AddToCart(int albumId) {
            var albumIds = db.Albums.Select(a => a.AlbumId).ToList();

            if(albumIds.Any(a => a == albumId) ) {
                Cart cartItem = db.Carts.SingleOrDefault(c => c.CartId == this.ShoppingCartId && c.AlbumId == albumId);
                if (cartItem == null) {
                    cartItem = new Cart() {
                        CartId = this.ShoppingCartId,
                        AlbumId = albumId,
                        Count = 1,
                        DateCreated = DateTime.Now
                    };

                    db.Carts.Add(cartItem);
                } else {
                    cartItem.Count++;
                }
                db.SaveChanges();
            }
        }

        public int RemoveFromCart(int recordId) {
            Cart cartItem = db.Carts.SingleOrDefault(c => c.CartId == this.ShoppingCartId && c.RecordId == recordId);

            if (cartItem == null) {
                throw new NullReferenceException();
            }

            int newCount;

            if (cartItem.Count > 1) {
                cartItem.Count--;
                newCount = cartItem.Count;
            }
            else{
                db.Carts.Remove(cartItem);
                newCount = 0;
            }
            db.SaveChanges();

            return newCount;
        }
    }
}