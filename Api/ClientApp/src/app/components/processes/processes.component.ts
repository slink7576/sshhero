import { Component, OnInit, ViewChild } from "@angular/core";
import { CommandClient, ProcessInfo, Credentials, GetAllProcessesCommand } from "src/api";
import { MatTableDataSource, MatSort } from "@angular/material";

@Component({
    selector:'processes-component', 
    templateUrl: './processes.component.html',
    styleUrls: ['./processes.component.css']
})

export class ProcessesComponent implements OnInit{

    processes = new Array<ProcessInfo>();
    currentServer = new Credentials();
    error = '';
    displayedColumns: string[] = ['id', 'name', 'cpu', 'memory', 'kill'];
    dataSource: MatTableDataSource<ProcessInfo>;
  
    @ViewChild(MatSort) sort: MatSort;

    constructor(private client: CommandClient){}
    ngOnInit(){

    }
    onChangeServer(event){
        this.currentServer = event;
        let comm = new GetAllProcessesCommand();
        comm.credentials = new Credentials(this.currentServer);
        this.client.getAllProcesses(comm).subscribe(data => {
            if(!data.isError){
                this.error = '';
                this.processes = data.processes;
                this.dataSource = new MatTableDataSource(this.processes);
                this.dataSource.sort = this.sort;
            }else{
                this.processes = new Array<ProcessInfo>();
                this.error = data.error;
            }
        });
    }
    onKillProcess(a){
        console.log(a)
    }
}