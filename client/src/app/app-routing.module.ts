import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './account/register/register.component';
import { BilliardsComponent } from './games/billiards/billiards.component';
import { NotFoundComponent } from './dummy-components/not-found/not-found.component';
import { AuthGuard } from './_guards/auth.guard';
import { DummyMainComponent } from './dummy-components/dummy-main/dummy-main.component';
import { UserEditComponent } from './account/user-edit/user-edit.component';
import { ChangePasswordComponent } from './account/change-password/change-password.component';
import { ForgotPasswordComponent } from './account/forgot-password/forgot-password.component';
import { PhotoEditComponent } from './account/photo-edit/photo-edit.component';
import { UserManagementComponent } from './account/user-management/user-management.component';
import { AdminGuard } from './_guards/admin.guard';

// angular follows first match wins policy
const routes: Routes = [
  {path: '', component: DummyMainComponent},
  {path: 'billiards', component: BilliardsComponent, pathMatch: 'full'},
  {path: 'register', component: RegisterComponent, pathMatch: 'full'},
  {path: 'forgot', component: ForgotPasswordComponent, pathMatch: 'full'},
  {path: 'user/billiards', component: BilliardsComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard]},
  {path: 'user/photo-edit', component: PhotoEditComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard], pathMatch: 'full'},
  {path: 'user/user-management', component: UserManagementComponent, runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard, AdminGuard] , pathMatch: 'full'},
  {path: 'user/edit', component: UserEditComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard], pathMatch: 'full'},
  {path: 'user/changepassword', component: ChangePasswordComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard], pathMatch: 'full'},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'} // wildcard route (if user types something not in routes)
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
