import { Routes } from '@angular/router';
import { AppComponent } from '../App-Component/app.component';
import { LoginComponent } from '../pages/customer/login/login.component';
import { OverviewComponent } from '../pages/Invoices/overview/overview.component';
import { CreateInvoiceComponent } from '../pages/Invoices/create-invoice/create-invoice.component';
import { CreateCustomerComponent } from '../pages/customer/create-customer/create-customer.component';
import { CreateProductComponent } from '../pages/Products/create-product/create-product.component';


export const routes: Routes = [
    {path: 'login', component: LoginComponent},
    {path: 'overview/:id', component: OverviewComponent},
    {path: 'create-invoice/:id', component: CreateInvoiceComponent},
    {path: 'create-customer',component: CreateCustomerComponent},
    {path: 'create-product',component: CreateProductComponent},
    {path: '' , component: CreateCustomerComponent},
];