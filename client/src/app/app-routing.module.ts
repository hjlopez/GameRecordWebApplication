import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './account/register/register.component';
import { BilliardsComponent } from './games/billiards/billiards.component';
import { LoggedInComponent } from './home/logged-in/logged-in.component';
import { NotFoundComponent } from './dummy-components/not-found/not-found.component';
import { AuthGuard } from './_guards/auth.guard';
import { DummyMainComponent } from './dummy-components/dummy-main/dummy-main.component';
import { UserEditComponent } from './account/user-edit/user-edit.component';

// angular follows first match wins policy
const routes: Routes = [
  {path: '', component: DummyMainComponent},
  {path: 'billiards', component: BilliardsComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'user/billiards', component: BilliardsComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard]},
  {path: 'user/edit', component: UserEditComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard], pathMatch: 'full'},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'} // wildcard route (if user types something not in routes)
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
