import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { CustomMaterialModule } from '../material-module';
import { CommandClient } from 'src/api';
import {  HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ConsoleComponent } from './components/console/console.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { SettingsComponent } from './components/settings/settings.component';
import { ServersService } from './services/servers.service';
import { ProcessesComponent } from './components/processes/processes.component';
import { SharedMachineSelectorComponent } from './components/shared/machine-selector/shared-machine-selector.component';
import { FilesComponent } from './components/files/files.component';
import { SharedCreateFileModel } from './components/shared/create-file-modal/shared-create-file-modal';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    ConsoleComponent,
    SidebarComponent,
    SettingsComponent,
    ProcessesComponent,
    FilesComponent,
    SharedMachineSelectorComponent,
    SharedCreateFileModel,
    
  ],
  imports: [
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BrowserModule,
    CustomMaterialModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(
      [
        { path: '', component: ConsoleComponent},
        { path: 'console', component: ConsoleComponent},
        { path: 'settings', component: SettingsComponent},
        { path: 'processes', component: ProcessesComponent},
        { path: 'files', component: FilesComponent},
      //  { path: '**', component: PageNotFoundComponent }
      ]
    )
  ],
  entryComponents: [SharedCreateFileModel],
  providers: [CommandClient, ServersService],
  bootstrap: [AppComponent]
})
export class AppModule { }
