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
import { FormBuilder, Validators } from '@angular/forms';
var CommandsEditorComponent = /** @class */ (function () {
    function CommandsEditorComponent(_formBuilder, client) {
        this._formBuilder = _formBuilder;
        this.client = client;
        this.result = '...';
    }
    CommandsEditorComponent.prototype.ngOnInit = function () {
        this.commandFormGroup = this._formBuilder.group({
            Command: ['', Validators.required]
        });
    };
    CommandsEditorComponent.prototype.onSendCommand = function () {
        var _this = this;
        var command = new ExecuteCustomCommand();
        command.command = this.commandFormGroup.controls['Command'].value;
        var credentials = new Credentials();
        credentials.hostname = '192.168.1.33';
        credentials.password = 'slinkonline2';
        credentials.login = 'slink7576';
        command.credentials = credentials;
        this.client.executeCustom(command).subscribe(function (data) {
            console.log(data);
            _this.result = data.result;
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