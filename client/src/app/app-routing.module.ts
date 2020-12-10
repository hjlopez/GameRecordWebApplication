import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoggedInComponent } from './home/logged-in/logged-in.component';
import { LoggedOutComponent } from './home/logged-out/logged-out.component';
import { NavComponent } from './nav/nav.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { AuthGuard } from './_guards/auth.guard';

// angular follows first match wins policy
const routes: Routes = [
  {path: '', component: LoggedOutComponent},
  {path: 'welcome', component: LoggedInComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard]},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'} // wildcard route (if user types something not in routes)
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
