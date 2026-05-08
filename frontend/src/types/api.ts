export type ApiResponse<T> = {
  Success?: boolean;
  Data?: T;
  Message?: string;
  ErrorCode?: string | null;
};
