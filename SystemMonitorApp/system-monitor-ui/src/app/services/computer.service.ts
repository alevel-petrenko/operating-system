import { Injectable } from '@angular/core';
import { DataApiService } from './data-api.service';
import { Observable } from 'rxjs';
import { withCache } from '@ngneat/cashew';

export interface ComputerInfo {
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class ComputerService {

  constructor(private apiService: DataApiService) { }

  public getComputerName(): Observable<ComputerInfo> {
    return this.apiService.get<ComputerInfo>('computer/name')
      // withCache({ ttl: 60000 }));
  }
}
