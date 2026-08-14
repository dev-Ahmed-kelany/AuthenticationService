import { AppError } from './app-error.model';

export class Result<TData> {
  private constructor(
    public readonly isSuccess: boolean,
    public readonly error: AppError,
    public readonly data?: TData,
  ) {}

  static Success<TData>(data: TData): Result<TData> {
    return new Result<TData>(true, AppError.None, data);
  }

  static Failure<TData>(error: AppError): Result<TData> {
    return new Result<TData>(false, error);
  }
}
