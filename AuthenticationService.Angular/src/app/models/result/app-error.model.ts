export class AppError {
  constructor(
    public readonly code: string,
    public readonly description: string,
    public readonly statusCode: number,
  ) {}

  static readonly None = new AppError('', '', 0);
}
