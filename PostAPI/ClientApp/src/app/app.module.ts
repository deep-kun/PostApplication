import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AuthenticationComponent } from './authentication/authentication.component';
import { AuthGuardService } from './services/auth-guard.service';
import { MessagesComponent } from './post/messages/messages.component';
import { JwtInterceptorService } from './services/jwt-interceptor.service';
import { MessageDetailsComponent } from './post/message-details/message-details.component';
import { MessageCreateComponent } from './post/message-create/message-create.component';
import { RegistrationComponent } from './authentication/registration/registration.component';
import { AuthenticationService } from './services/authentication.service';
import { ErrorInterceptorService } from './services/error-interceptor.service';
import { UserListComponent } from './administration/user-list/user-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AuthenticationComponent,
    MessagesComponent,
    MessageDetailsComponent,
    MessageCreateComponent,
    RegistrationComponent,
    UserListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuardService] },
      { path: 'auth', component: AuthenticationComponent },
      { path: 'messages', component: MessagesComponent, canActivate: [AuthGuardService] },
      { path: 'newmsg', component: MessageCreateComponent, canActivate: [AuthGuardService] },
      { path: 'reg', component: RegistrationComponent },
      { path: 'users', component: UserListComponent, canActivate: [AuthGuardService] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
    AuthenticationService 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
