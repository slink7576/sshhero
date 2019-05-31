import { Component, OnInit } from "@angular/core";
import { CommandClient, ProcessInfo } from "src/api";

@Component({
    selector:'processes-component', 
    templateUrl: './processes.component.html',
    styleUrls: ['./processes.component.css']
})

export class ProcessesComponent implements OnInit{

    processes = new Array<ProcessInfo>();

    constructor(private client: CommandClient){}
    ngOnInit(){
        
    }
}