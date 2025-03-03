namespace Cafeteria_Management;

public class BubbleSort
{
    public static void Sort(MenuItem[] array)
    {
        int n = array.Length;
        MenuItem tmp;
        bool swapped;

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j].Price > array[j + 1].Price)
                {
                    tmp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = tmp;
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
    }
}
public class QuickSort
{
    public static void Sort(MenuItem[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivot = Partition(arr, low, high);
            Sort(arr, low, pivot - 1);
            Sort(arr, pivot + 1, high);
        }
    }

    private static int Partition(MenuItem[] arr, int low, int high)
    {
        double pivot = arr[high].Price;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j].Price < pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    private static void Swap(MenuItem[] arr, int i, int j)
    {
        MenuItem tmp = arr[i];
        arr[i] = arr[j];
        arr[j] = tmp;
    }
}
public class MergeSort
{
    public static void Sort(MenuItem[] arr, int left, int right)
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;

            Sort(arr, left, middle);
            Sort(arr, middle + 1, right);

            Merge(arr, left, middle, right);
        }
    }

    private static void Merge(MenuItem[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        MenuItem[] L = new MenuItem[n1];
        MenuItem[] R = new MenuItem[n2];

        Array.Copy(arr, left, L, 0, n1);
        Array.Copy(arr, mid + 1, R, 0, n2);

        int i = 0, j = 0, k = left;

        while (i < n1 && j < n2)
        {
            if (L[i].Price <= R[j].Price)
            {
                arr[k] = L[i];
                i++;
            }
            else
            {
                arr[k] = R[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            arr[k] = L[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;
        }
    }
}
