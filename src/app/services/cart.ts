import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../models/api-response';
import { Cart } from '../models/cart';

@Injectable({
  providedIn: 'root',
})
export class CartService
{
  private baseUrl = `${environment.apiUrl}/carts`;
  constructor(private http: HttpClient) { }
  getAllCart(userId: number) {
  return this.http.get<ApiResponse<Cart>>(
    `http://localhost:5155/api/Cart/${userId}`
  );
}
}
