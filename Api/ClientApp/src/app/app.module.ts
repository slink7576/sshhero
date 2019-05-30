import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { CustomMaterialModule } from '../material-module';
import { ReactiveFormsModule } from '@angular/forms';
import { CommandClient } from 'src/api';
import {  HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ConsoleComponent } from './components/console/console.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { SettingsComponent } from './components/settings/settings.component';
import { ServersService } from './services/servers.service';


@NgModule({
  declarations: [
    AppComponent,
    ConsoleComponent,
    SidebarComponent,
    SettingsComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BrowserModule,
    CustomMaterialModule,
    HttpClientModule,
    RouterModule.forRoot(
      [
        { path: '', component: ConsoleComponent},
        { path: 'console', component: ConsoleComponent},
        { path: 'settings', component: SettingsComponent},
      //  { path: '**', component: PageNotFoundComponent }
      ]
    )
  ],
  providers: [CommandClient, ServersService],
  bootstrap: [AppComponent]
})
export class AppModule { }
