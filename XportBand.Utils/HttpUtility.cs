//-----------------------------------------------------------------------
// <copyright file="HttpUtility.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;

namespace System.Web
{

    #region HttpUtility class

    /// <summary>
    /// Provides methods for encoding and decoding URLs when processing Web requests.
    /// </summary>
    /// <remarks>
    /// This class cannot be inherited. This is a custom implementation for standard class System.Web.HttpUtility.
    /// </remarks>
    public sealed class HttpUtility
    {

        #region Methods

        /// <summary>
        /// Parses a query string into a <see cref="HttpValueCollection"/>.
        /// </summary>
        /// <param name="query">The query string to parse.</param>
        /// <returns>A <see cref="HttpValueCollection"/> of query parameters and values.</returns>
        public static HttpValueCollection ParseQueryString(string query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if ((query.Length > 0) && (query[0] == '?'))
            {
                query = query.Substring(1);
            }

            return new HttpValueCollection(query, true);
        }

        #endregion

    }

    #endregion

    #region HttpValue class

    /// <summary>
    /// Represents a associated <see cref="string"/> key and <see cref="string"/> value.
    /// </summary>
    public sealed class HttpValue
    {

        #region Properties

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="HttpValue"/>.
        /// </summary>
        public HttpValue()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpValue"/> with the specified name and value.
        /// </summary>
        /// <param name="key">The <see cref="string"/> key of the entry.</param>
        /// <param name="value">The <see cref="string"/> value of the entry.</param>
        public HttpValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        #endregion

    }

    #endregion

    #region HttpValueCollection class

    /// <summary>
    /// Represents a collection of associated keys and values (<see cref="HttpValue"/>) that can 
    /// be accessed either with the key or with the index.
    /// </summary>
    public class HttpValueCollection : Collection<HttpValue>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpValueCollection"/> class that is empty.
        /// </summary>
        public HttpValueCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpValueCollection"/> class and fills 
        /// from <see cref="System.Uri.Query"/> string.
        /// </summary>
        /// <param name="query">The <see cref="System.Uri.Query"/> string.</param>
        public HttpValueCollection(string query)
            : this(query, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpValueCollection"/> class and fills 
        /// from <see cref="System.Uri.Query"/> string.
        /// </summary>
        /// <param name="query">The <see cref="System.Uri.Query"/> string.</param>
        /// <param name="urlencoded">Flag to indicate if <paramref name="query"/> is URL encoded.</param>
        public HttpValueCollection(string query, bool urlencoded)
        {
            if (!string.IsNullOrEmpty(query))
            {
                this.FillFromString(query, urlencoded);
            }
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets or sets the <see cref="HttpValue.Value"/> from its <see cref="HttpValue.Key"/>.
        /// </summary>
        /// <param name="key">The <see cref="string"/> key of the entry to locate.</param>
        /// <returns>The <see cref="HttpValue.Value"/> located.</returns>
        public string this[string key]
        {
            get { return this.First(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).Value; }
            set { this.First(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).Value = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an entry with the specified key and value.
        /// </summary>
        /// <param name="key">The <see cref="string"/> key of the entry to add</param>
        /// <param name="value">The <see cref="string"/> value of the entry to add</param>
        public void Add(string key, string value)
        {
            this.Add(new HttpValue(key, value));
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="HttpValueCollection"/> contains the specified key.
        /// </summary>
        /// <param name="key">The <see cref="string"/> key to locate.</param>
        /// <returns><see langword="true"/> if the <see cref="HttpValueCollection"/> contains the specified key; otherwise, <see langword="false"/>.</returns>
        public bool ContainsKey(string key)
        {
            return this.Any(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets the values associated with the specified key from the <see cref="HttpValueCollection"/>.
        /// </summary>
        /// <param name="key">The <see cref="string"/> key of the entry that contains the values to get.</param>
        /// <returns>A <see cref="string"/> array that contains the values associated with the specified key from the 
        /// <see cref="HttpValueCollection"/>, if found; otherwise, <see langword="null"/>.</returns>
        public string[] GetValues(string key)
        {
            return this.Where(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToArray();
        }

        /// <summary>
        /// Removes the entries with the specified key from the <see cref="HttpValueCollection"/> instance.
        /// </summary>
        /// <param name="key">The <see cref="string"/> key of the entries to remove.</param>
        public void Remove(string key)
        {
            //this.Where(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase))
            //    .ToList()
            //    .ForEach(x => this.Remove(x));
            foreach (HttpValue item in this.Where(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)))
            {
                this.Remove(item);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToString(true);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="urlencoded">Flag to indicate that returned string must be URL encoded.</param>
        /// <returns>A string that represents the current object.</returns>
        public virtual string ToString(bool urlencoded)
        {
            return ToString(urlencoded, null);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="urlencoded">Flag to indicate that returned string must be URL encoded.</param>
        /// <param name="excludeKeys">A collection of keys to exclude in string returned.</param>
        /// <returns>A string that represents the current object.</returns>
        public virtual string ToString(bool urlencoded, IDictionary excludeKeys)
        {
            if (this.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (HttpValue item in this)
            {
                string key = item.Key;

                if ((excludeKeys == null) || !excludeKeys.Contains(key))
                {
                    string value = item.Value;

                    if (urlencoded)
                    {
                        // If .NET 4.5 and above (Thanks @Paya)
                        key = WebUtility.UrlDecode(key);
                        // If .NET 4.0 use this instead.
                        // key = Uri.EscapeDataString(key);
                    }

                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append('&');
                    }

                    stringBuilder.Append((key != null) ? (key + "=") : string.Empty);

                    if ((value != null) && (value.Length > 0))
                    {
                        if (urlencoded)
                        {
                            value = Uri.EscapeDataString(value);
                        }

                        stringBuilder.Append(value);
                    }
                }
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fills current <see cref="HttpValueCollection"/> from query string.
        /// </summary>
        /// <param name="query">The <see cref="System.Uri.Query"/> string.</param>
        /// <param name="urlencoded">Flag to indicate if <paramref name="query"/> is URL encoded.</param>
        private void FillFromString(string query, bool urlencoded)
        {
            int num = (query != null) ? query.Length : 0;
            for (int i = 0; i < num; i++)
            {
                int startIndex = i;
                int num4 = -1;
                while (i < num)
                {
                    char ch = query[i];
                    if (ch == '=')
                    {
                        if (num4 < 0)
                        {
                            num4 = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }
                    i++;
                }
                string str = null;
                string str2 = null;
                if (num4 >= 0)
                {
                    str = query.Substring(startIndex, num4 - startIndex);
                    str2 = query.Substring(num4 + 1, (i - num4) - 1);
                }
                else
                {
                    str2 = query.Substring(startIndex, i - startIndex);
                }

                if (urlencoded)
                {
                    this.Add(Uri.UnescapeDataString(str), Uri.UnescapeDataString(str2));
                }
                else
                {
                    this.Add(str, str2);
                }

                if ((i == (num - 1)) && (query[i] == '&'))
                {
                    this.Add(null, string.Empty);
                }
            }
        }

        #endregion

    }

    #endregion

}
