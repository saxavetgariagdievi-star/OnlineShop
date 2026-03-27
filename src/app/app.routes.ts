import { Routes } from '@angular/router';
import { ProductComponent, } from './components/product/product';
import { CartComponent } from './components/cart/cart';

export const routes: Routes = [
    { path: "", component: ProductComponent },
     {path: 'cart', component: CartComponent}
];
