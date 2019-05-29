var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { ExecuteCustomCommand, CommandClient, Credentials } from 'src/api';
import { FormBuilder } from '@angular/forms';
var CommandsEditorComponent = /** @class */ (function () {
    function CommandsEditorComponent(_formBuilder, client) {
        this._formBuilder = _formBuilder;
        this.client = client;
        this.result = 'Hello world!';
        this.index = 0;
        this.commandsHistory = new Array();
    }
    CommandsEditorComponent.prototype.ngOnInit = function () {
        this.commandFormGroup = this._formBuilder.group({
            Command: ['',]
        });
    };
    CommandsEditorComponent.prototype.onClear = function () {
        this.result = '';
        this.error = '';
    };
    CommandsEditorComponent.prototype.onLoadCommandUp = function () {
        this.index--;
        if (this.index < 0) {
            this.index = 0;
        }
        this.commandFormGroup.controls['Command'].setValue(this.commandsHistory[this.index]);
    };
    CommandsEditorComponent.prototype.onLoadCommandDown = function () {
        this.index++;
        if (this.index >= this.commandsHistory.length) {
            this.index = this.commandsHistory.length - 1;
        }
        this.commandFormGroup.controls['Command'].setValue(this.commandsHistory[this.index]);
    };
    CommandsEditorComponent.prototype.onSendCommand = function () {
        var _this = this;
        this.commandsHistory.push(this.commandFormGroup.controls['Command'].value);
        this.index = this.commandsHistory.length;
        console.log(this.commandsHistory);
        var command = new ExecuteCustomCommand();
        command.command = this.commandFormGroup.controls['Command'].value;
        this.commandFormGroup.controls['Command'].setValue('');
        var credentials = new Credentials();
        credentials.hostname = '192.168.1.33';
        credentials.password = 'slinkonline2';
        credentials.login = 'slink7576';
        command.credentials = credentials;
        this.client.executeCustom(command).subscribe(function (data) {
            if (data.isError) {
                _this.result = '';
                _this.error = data.error;
            }
            else {
                _this.result = data.result;
                _this.error = '';
            }
        });
    };
    CommandsEditorComponent = __decorate([
        Component({
            selector: 'commands-editor-component',
            templateUrl: './commands-editor.component.html',
            styleUrls: ['./commands-editor.component.css']
        }),
        __metadata("design:paramtypes", [FormBuilder, CommandClient])
    ], CommandsEditorComponent);
    return CommandsEditorComponent;
}());
export { CommandsEditorComponent };
//# sourceMappingURL=commands-editor.component.js.map