export interface CartItem
{
    id: number;
    productId: number;
    name: string;
    price: number;
    quantity: number;
    img?: string;
    
}
export interface Cart
{
    id: number;
    userId: number;
    cartItems: CartItem[];
}