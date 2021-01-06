import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/billiards/Pagination';

export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient): Observable<PaginatedResult<T>>
{
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    // observing a get method returns the whole response
    return http.get<T>(url, {observe: 'response', params}).pipe(
        map(response => {
            paginatedResult.result = response.body || paginatedResult.result;
            if (response.headers.get('Pagination') !== null)
            {
                paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '');
            }

            return paginatedResult;
        })
    );
}

export function getPaginationHeaders(pageNumber: number, pageSize: number): HttpParams
{
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
}
