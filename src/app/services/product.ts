import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService
{
  private baseUrl = `${environment.apiUrl}/products`;
  constructor(private http: HttpClient) { }
  getAll(): Observable<ApiResponse<Product[]>>
  {
    return this.http.get<ApiResponse<Product[]>>(this.baseUrl);
  }
  getById(id: number): Observable<ApiResponse<Product>>
  {
    return this.http.get<ApiResponse<Product>>(`${this.baseUrl}/${id}`);
  }
}
