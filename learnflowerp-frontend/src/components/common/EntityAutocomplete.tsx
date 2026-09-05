import { Autocomplete, CircularProgress, TextField } from "@mui/material";
import { useCallback, useEffect, useState } from "react";

type Props<T> = {
  label: string;
  value: T | null;
  onChange: (value: T | null) => void;
  loadOptions: (search: string) => Promise<T[]>;
  getOptionLabel: (option: T) => string;
  getOptionKey: (option: T) => string | number;
  disabled?: boolean;
  exclude?: (option: T) => boolean;
};

export default function EntityAutocomplete<T>({
  label, value, onChange, loadOptions, getOptionLabel, getOptionKey, disabled = false, exclude,
}: Props<T>) {
  const [options, setOptions] = useState<T[]>([]);
  const [loading, setLoading] = useState(false);

  const fetchOptions = useCallback(async (search: string) => {
    try {
      setLoading(true);
      setOptions((await loadOptions(search)).filter((option) => !exclude?.(option)));
    } finally {
      setLoading(false);
    }
  }, [exclude, loadOptions]);

  useEffect(() => { void fetchOptions(""); }, [fetchOptions]);

  return (
    <Autocomplete
      fullWidth
      value={value}
      options={options}
      loading={loading}
      disabled={disabled}
      onChange={(_event, nextValue) => onChange(nextValue)}
      onInputChange={(_event, inputValue, reason) => { if (reason === "input") void fetchOptions(inputValue); }}
      getOptionLabel={getOptionLabel}
      isOptionEqualToValue={(option, selected) => getOptionKey(option) === getOptionKey(selected)}
      renderInput={(params) => <TextField {...params} label={label} InputProps={{ ...params.InputProps, endAdornment: <>{loading ? <CircularProgress color="inherit" size={18} /> : null}{params.InputProps.endAdornment}</> }} />}
    />
  );
}
