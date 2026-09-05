import api from "./axios";

export const uploadFileApi = async (file: File) => {
  const formData = new FormData();

  formData.append("file", file);

  const response = await api.post("/files/upload", formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });

  return response.data;
};

export const deleteFileApi = async (url: string) => {
  const response = await api.delete("/files/delete", {
    params: { url },
  });

  return response.data;
};
