import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_API } from '../token/baseUrl.token';
import { Inject, Injectable } from '@angular/core';

@Injectable()
export class apiInterceptor implements HttpInterceptor {
  constructor(@Inject(BASE_API) private apiUrl: string) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (!request.url.startsWith('http')) {
      const apiReq = request.clone({
        url: `${this.apiUrl}/${request.url}`,
      })

      return next.handle(apiReq);
    }

    return next.handle(request);
  }
}
