import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { CustomMaterialModule } from '../material-module';
import { ReactiveFormsModule } from '@angular/forms';
import { CommandsEditorComponent } from './components/commands/editor/commands-editor.component';
import { CommandClient } from 'src/api';
import {  HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    CommandsEditorComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BrowserModule,
    CustomMaterialModule,
    HttpClientModule
  ],
  providers: [CommandClient,],
  bootstrap: [AppComponent]
})
export class AppModule { }
