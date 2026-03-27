import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product';
import { Product } from '../../models/product';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [CommonModule],
  templateUrl: './product.html',
  styleUrl: './product.css',
})
export class ProductComponent implements OnInit
{
  products: Product[] = [];
  constructor(private productService: ProductService) { }
  ngOnInit(): void
  {
    this.getProducts();
    
  }
  getProducts()
  {
      this.productService.getAll().subscribe({
      next: (res) =>
      {
        if (res.status)
        {
          this.products = res.data;
          console.log(this.products);
        }
      }
    })
  }
  
}
  


