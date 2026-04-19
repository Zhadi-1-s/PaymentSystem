import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  // Public routes
  { path: 'login',    component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  // Protected routes — AuthGuard checks JWT before allowing access
  // { path: 'payments', component: PaymentsComponent, canActivate: [AuthGuard] },
  { path: 'profile',  component: ProfileComponent,  canActivate: [AuthGuard] },

  // Redirects
  { path: '',         redirectTo: 'login', pathMatch: 'full' },
  // { path: '**',       redirectTo: 'payments' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
