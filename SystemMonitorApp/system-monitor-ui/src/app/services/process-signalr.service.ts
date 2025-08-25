import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { RouteConstants } from '../route.constants';
import { ProcessInfo } from '../models/ProcessInfo';

@Injectable({
    providedIn: 'root'
})
export class ProcessSignalRService {
    private hubConnection!: signalR.HubConnection;

    startConnection() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${RouteConstants.baseUrl}hubs/processes`)
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .then(() => console.log('SignalR Connected'))
            .catch(err => console.error('SignalR Error: ', err));
    }

    onProcessesUpdated(callback: (processes: ProcessInfo[]) => void) {
        this.hubConnection.on('ProcessesUpdated', callback);
    }
}
