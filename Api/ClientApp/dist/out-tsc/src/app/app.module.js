var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { CustomMaterialModule } from '../material-module';
import { ReactiveFormsModule } from '@angular/forms';
import { CommandsEditorComponent } from './components/commands/editor/commands-editor.component';
import { CommandClient } from 'src/api';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        NgModule({
            declarations: [
                AppComponent,
                CommandsEditorComponent
            ],
            imports: [
                ReactiveFormsModule,
                BrowserAnimationsModule,
                BrowserModule,
                CustomMaterialModule,
                HttpClientModule,
                RouterModule.forRoot([
                    { path: '', component: CommandsEditorComponent },
                    { path: 'console', component: CommandsEditorComponent },
                ])
            ],
            providers: [CommandClient,],
            bootstrap: [AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map