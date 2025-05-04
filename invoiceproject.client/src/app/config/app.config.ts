import { Routes } from '@angular/router';
import { AppComponent } from '../App-Component/app.component';
import { LoginComponent } from '../pages/customer/login/login.component';
import { OverviewComponent } from '../pages/Invoices/overview/overview.component';


export const routes: Routes = [
    {path: 'login', component: LoginComponent},
    {path: 'overview/:id', component: OverviewComponent},
    {path: '' , component: AppComponent},
];