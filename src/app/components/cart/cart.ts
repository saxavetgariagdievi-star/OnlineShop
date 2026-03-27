import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart';
import { Cart, CartItem } from '../../models/cart';
import { ApiResponse } from '../../models/api-response';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.html',
  styleUrls: ['./cart.css'],
  standalone: true,
  imports: [CommonModule]
})
export class CartComponent implements OnInit {

  cartItems: CartItem[] = [];
  userId = 2;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }


  loadCart()
  {
    this.cartService.getAllCart(this.userId).subscribe({
      next: (res: ApiResponse<Cart>) => {
        console.log(res); // debug üçün
        if (res.status) {
          this.cartItems = res.data.cartItems; // 🔥 əsas hissə
        }
      },
      error: (err) => console.error(err)
    });
  }
   getTotal(): number {
    return this.cartItems.reduce((sum, i) => sum + i.price * i.quantity, 0);
  }
}
