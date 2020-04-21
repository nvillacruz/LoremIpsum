namespace LoremIpsum.Core
{
    public class ApiResponse
    {
        #region Public Properties

        /// <summary>
        /// Indicates if the API call was successful
        /// </summary>
        public bool Successful => ErrorMessage == null;

        /// <summary>
        /// The error message for a failed API call
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The API response object
        /// </summary>
        public object Response { get; set; }

        #endregion

        /// <summary>
        /// Returns a successful instance of <see cref="ApiResponse"/> with no Response object
        /// </summary>
        public static ApiResponse Success => new ApiResponse { Response = null };

        /// <summary>
        /// Returns a non-successful instance of <see cref="ApiResponse"/> with no Response object
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static ApiResponse Failed(string error)
        {
            var result = new ApiResponse();
            if (error != null)
            {
                result.ErrorMessage = error;
            }
            return result;
        }

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApiResponse()
        {

        }

        #endregion
    }

    /// <summary>
    /// The response for all Web API calls made
    /// with a specific type of known response
    /// </summary>
    /// <typeparam name="T">The specific type of server response</typeparam>
    public class ApiResponse<T> : ApiResponse
    {

        /// <summary>
        /// The API response object as T
        /// </summary>
        public new T Response { get => (T)base.Response; set => base.Response = value; }

        /// <summary>
        /// Returns a successful instance of <see cref="ApiResponse"/> with no Response object
        /// </summary>
        public new static ApiResponse<T> Success => new ApiResponse<T> { Response = default(T) };

        /// <summary>
        /// Returns a non-successful instance of <see cref="ApiResponse"/> with no Response object
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public new static ApiResponse<T> Failed(string error)
        {
            var result = new ApiResponse<T>();
            if (error != null)
            {
                result.ErrorMessage = error;
            }
            return result;
        }
    }

}
