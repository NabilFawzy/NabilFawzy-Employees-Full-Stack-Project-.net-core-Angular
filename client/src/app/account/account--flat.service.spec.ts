import { TestBed } from '@angular/core/testing';

import { AccountFlatService } from './account--flat.service';

describe('AccountFlatService', () => {
  let service: AccountFlatService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountFlatService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
