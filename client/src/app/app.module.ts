import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { LoggedInComponent } from './home/logged-in/logged-in.component';
import { LoggedOutComponent } from './home/logged-out/logged-out.component';
import { ToastrModule } from 'ngx-toastr';
import { NotFoundComponent } from './dummy-components/not-found/not-found.component';
import { NavLoggedComponent } from './nav-logged/nav-logged.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { TextInputsComponent } from './_forms/text-inputs/text-inputs.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RegisterComponent } from './account/register/register.component';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { BilliardsComponent } from './games/billiards/billiards.component';
import { DummyMainComponent } from './dummy-components/dummy-main/dummy-main.component';
import { UrlSerializer } from '@angular/router';
import { LowerCaseUrlSerializer } from './_serializer/LowerCaseUrlSerializer';
import { UserEditComponent } from './account/user-edit/user-edit.component';
import { ChangePasswordComponent } from './account/change-password/change-password.component';
import { ForgotPasswordComponent } from './account/forgot-password/forgot-password.component';
import { PhotoEditComponent } from './account/photo-edit/photo-edit.component';
import { FileUploadModule } from 'ng2-file-upload';
import { UserManagementComponent } from './account/user-management/user-management.component';
import { DeleteUserModalComponent } from './account/delete-user-modal/delete-user-modal.component';
import { UpdateUserModalComponent } from './account/update-user-modal/update-user-modal.component';
import { BilliardsSettingsComponent } from './games/billiards/settings/billiards-settings/billiards-settings.component';
import { TournamentModalComponent } from './games/billiards/settings/billiards-settings/tournament-modal/tournament-modal.component';
import { MemberModalComponent } from './games/billiards/settings/billiards-settings/member-modal/member-modal.component';
import { TypeModalComponent } from './games/billiards/settings/billiards-settings/type-modal/type-modal.component';
import { ModesModalComponent } from './games/billiards/settings/billiards-settings/modes-modal/modes-modal.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NewGameComponent } from './games/billiards/new-game/new-game.component';
import { AddSeasonComponent } from './games/billiards/add-season/add-season.component';
import { ViewGameComponent } from './games/billiards/view-game/view-game.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ConfirmModalComponent } from './confirm-modal/confirm-modal.component';
import { IndividualStatsComponent } from './games/billiards/individual-stats/individual-stats.component';
import { TournamentStatsComponent } from './games/billiards/tournament-stats/tournament-stats.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DatePipe } from '@angular/common';
import { PbaComponent } from './games/pba/pba.component';
import { PbaSettingsComponent } from './games/pba/pba-settings/pba-settings.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoggedInComponent,
    LoggedOutComponent,
    NotFoundComponent,
    NavLoggedComponent,
    TextInputsComponent,
    RegisterComponent,
    SideMenuComponent,
    BilliardsComponent,
    DummyMainComponent,
    UserEditComponent,
    ChangePasswordComponent,
    ForgotPasswordComponent,
    PhotoEditComponent,
    UserManagementComponent,
    DeleteUserModalComponent,
    UpdateUserModalComponent,
    BilliardsSettingsComponent,
    TournamentModalComponent,
    MemberModalComponent,
    TypeModalComponent,
    ModesModalComponent,
    NewGameComponent,
    AddSeasonComponent,
    ViewGameComponent,
    ConfirmModalComponent,
    IndividualStatsComponent,
    TournamentStatsComponent,
    GeneralSettingsComponent,
    PbaComponent,
    PbaSettingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    FileUploadModule,
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    PaginationModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: UrlSerializer, useClass: LowerCaseUrlSerializer},
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
