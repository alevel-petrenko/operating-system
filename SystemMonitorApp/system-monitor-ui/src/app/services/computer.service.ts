import { Injectable } from '@angular/core';
import { DataApiService } from '../data-api.service';
import { Observable } from 'rxjs';
import { withCache } from '@ngneat/cashew';

@Injectable({
  providedIn: 'root'
})
export class ComputerService {

  constructor(private apiService: DataApiService) { }

  public getComputerName(): Observable<string> {
    return this.apiService.get<string>('computer/name',
      withCache({ ttl: 60000 }));
  }
}
