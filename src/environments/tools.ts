import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToolsService
{
    constructor(public http: HttpClient) { }
    getAllProduct()
    {
        return this.http.get("http://localhost:5155/api/Products");
    }
    getProductById(id: number)
    {
        return this.http.get(`http://localhost:5155/api/Products/Product/${id}`);
    }
    getAllCart(userId: number)
    {
        return this.http.get(`http://localhost:5155/api/Cart/${userId}`);
    }
}
